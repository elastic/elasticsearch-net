using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkApiTests : ApiIntegrationTestBase<WritableCluster, IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Bulk(f),
			fluentAsync: (client, f) => client.BulkAsync(f),
			request: (client, r) => client.Bulk(r),
			requestAsync: (client, r) => client.BulkAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new object[]
		{
			new Dictionary<string, object>{ { "index", new {  _type = "project", _id = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>{ { "update", new { _type="project", _id = Project.Instance.Name } } },
			new { doc = new { leadDeveloper = new { firstName = "martijn" } } } ,
			new Dictionary<string, object>{ { "create", new { _type="project", _id = Project.Instance.Name + "1" } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>{ { "delete", new { _type="project", _id = Project.Instance.Name + "1" } } },
			new Dictionary<string, object>{ { "create", new { _type="project", _id = Project.Instance.Name + "2" } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>{ { "update", new { _type="project", _id = Project.Instance.Name + "2" } } },
			new Dictionary<string, object>{ { "script", new
			{
				inline= "ctx._source.numberOfCommits = commits",
				@params = new { commits = 30 },
				lang = "groovy"
			} } },
		};

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Index<Project>(b => b.Document(Project.Instance))
			.Update<Project, object>(b => b.Doc(new { leadDeveloper = new { firstName = "martijn" } }).Id(Project.Instance.Name))
			.Create<Project>(b => b.Document(Project.Instance).Id(Project.Instance.Name + "1"))
			.Delete<Project>(b=>b.Id(Project.Instance.Name + "1"))
			.Create<Project>(b => b.Document(Project.Instance).Id(Project.Instance.Name + "2"))
			.Update<Project>(b => b
				.Id(Project.Instance.Name + "2")
				.Script("ctx._source.numberOfCommits = commits")
				.Params(p => p.Add("commits", 30))
				.Lang("groovy")
			);


		protected override BulkRequest Initializer =>
			new BulkRequest(CallIsolatedValue)
			{
				Operations = new List<IBulkOperation>
				{
					new BulkIndexOperation<Project>(Project.Instance),
					new BulkUpdateOperation<Project, object>(Project.Instance)
					{
						Doc = new { leadDeveloper = new { firstName = "martijn" } }
					},
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "1"
					},
					new BulkDeleteOperation<Project>(Project.Instance.Name + "1"),
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "2",
					},
					new BulkUpdateOperation<Project, object>(Project.Instance.Name + "2")
					{
						Script = "ctx._source.numberOfCommits = commits",
						Params = new Dictionary<string, object> { { "commits", 30 } },
						Lang = "groovy"
					}
				}
			};

		protected override void ExpectResponse(IBulkResponse response)
		{
			response.Took.Should().BeGreaterThan(0);
			response.Errors.Should().BeFalse();
			response.ItemsWithErrors.Should().NotBeNull().And.BeEmpty();
			response.Items.Should().NotBeEmpty();
			foreach (var item in response.Items)
			{
				item.Index.Should().Be(CallIsolatedValue);
				item.Type.Should().Be("project");
				item.Status.Should().BeGreaterThan(100);
				item.Version.Should().BeGreaterThan(0);
				item.Id.Should().NotBeNullOrWhiteSpace();
				item.IsValid.Should().BeTrue();
				item.Shards.Should().NotBeNull();
				item.Shards.Total.Should().BeGreaterThan(0);
				item.Shards.Successful.Should().BeGreaterThan(0);
			}

			var project1 = this.Client.Source<Project>(Project.Instance.Name, p => p.Index(CallIsolatedValue));
			project1.LeadDeveloper.FirstName.Should().Be("martijn");

			var project2 = this.Client.Source<Project>(Project.Instance.Name + "2", p => p.Index(CallIsolatedValue));
			project2.NumberOfCommits.Should().Be(30);
		}

	}
}
