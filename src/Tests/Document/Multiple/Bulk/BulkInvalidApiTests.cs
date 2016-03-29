using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.Bulk
{
	[Collection(IntegrationContext.Indexing)]
	public class BulkInvalidApiTests : ApiIntegrationTestBase<IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkInvalidApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Bulk(f),
			fluentAsync: (client, f) => client.BulkAsync(f),
			request: (client, r) => client.Bulk(r),
			requestAsync: (client, r) => client.BulkAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new object[]
		{
			new Dictionary<string, object>{ { "update", new { _type="project", _id = Project.Instance.Name } } },
			new { doc = new { leadDeveloper = new { firstName = "martijn" } } },
			new Dictionary<string, object>{ { "delete", new { _type="project", _id = Project.Instance.Name + "1" } } },
		};

		protected override void ExpectResponse(IBulkResponse response)
		{
			response.Took.Should().BeGreaterThan(0);
			response.Errors.Should().BeTrue();

			//a delete not found is not an error (also in Elasticsearch)
			//if you do a single bulk delete on an unknown id .Errors will be false
			response.ItemsWithErrors.Should().NotBeNull().And.HaveCount(1);
			response.Items.Should().NotBeEmpty();

			var failedUpdate = response.Items.First() as BulkUpdateResponseItem;
			failedUpdate.Should().NotBeNull();
			failedUpdate.Index.Should().Be(CallIsolatedValue);
			failedUpdate.Status.Should().Be(404);
			failedUpdate.Error.Should().NotBeNull();
			failedUpdate.Error.Type.Should().Be("document_missing_exception");
			failedUpdate.IsValid.Should().BeFalse();

			var failedDelete = response.Items.Last() as BulkDeleteResponseItem;
			failedDelete.Found.Should().BeFalse();
			failedDelete.IsValid.Should().BeTrue();
		}

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Update<Project, object>(b => b.Doc(new { leadDeveloper = new { firstName = "martijn" } }).Id(Project.Instance.Name))
			.Delete<Project>(b=>b.Id(Project.Instance.Name + "1"));


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = new List<IBulkOperation>
			{
				new BulkUpdateOperation<Project, object>(Project.Instance)
				{
					Doc = new { leadDeveloper = new { firstName = "martijn" } }
				},
				new BulkDeleteOperation<Project>(Project.Instance.Name + "1"),
			}
		};
	}
}
