// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkApiTests : ApiIntegrationTestBase<WritableCluster, BulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new[]
		{
			new Dictionary<string, object>
				{ { "index", new { _id = Project.Instance.Name, pipeline = "pipeline", routing = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object> { { "update", new { _id = Project.Instance.Name } } },
			new { doc = new { leadDeveloper = new { firstName = "martijn" } } },
			new Dictionary<string, object>
				{ { "create", new { _id = Project.Instance.Name + "1", routing = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>
				{ { "delete", new { _id = Project.Instance.Name + "1", routing = Project.Instance.Name } } },
			new Dictionary<string, object>
				{ { "create", new { _id = Project.Instance.Name + "2", routing = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>
				{ { "update", new { _id = Project.Instance.Name + "2", routing = Project.Instance.Name } } },
			new Dictionary<string, object>
			{
				{
					"script", new
					{
						source = "ctx._source.numberOfCommits = params.commits",
						@params = new { commits = 30 },
						lang = "painless"
					}
				}
			},
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Pipeline("default-pipeline")
			.Index<Project>(b => b.Document(Project.Instance).Pipeline("pipeline"))
			.Update<Project, object>(b => b.Doc(new { leadDeveloper = new { firstName = "martijn" } }).Id(Project.Instance.Name))
			.Create<Project>(b => b.Document(Project.Instance).Id(Project.Instance.Name + "1"))
			.Delete<Project>(b => b.Id(Project.Instance.Name + "1").Routing(Project.Instance.Name))
			.Create<Project>(b => b.Document(Project.Instance).Id(Project.Instance.Name + "2"))
			.Update<Project>(b => b
				.Id(Project.Instance.Name + "2")
				.Routing(Project.Instance.Name)
				.Script(s => s
					.Source("ctx._source.numberOfCommits = params.commits")
					.Params(p => p.Add("commits", 30))
					.Lang("painless")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override BulkRequest Initializer =>
			new BulkRequest(CallIsolatedValue)
			{
				Pipeline = "default-pipeline",
				Operations = new List<IBulkOperation>
				{
					new BulkIndexOperation<Project>(Project.Instance) { Pipeline = "pipeline" },
					new BulkUpdateOperation<Project, object>(Project.Instance.Name)
					{
						Doc = new { leadDeveloper = new { firstName = "martijn" } }
					},
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "1",
					},
					new BulkDeleteOperation<Project>(Project.Instance.Name + "1")
					{
						Routing = Project.Instance.Name
					},
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "2",
					},
					new BulkUpdateOperation<Project, object>(Project.Instance.Name + "2")
					{
						Routing = Project.Instance.Name,
						Script = new InlineScript("ctx._source.numberOfCommits = params.commits")
						{
							Params = new Dictionary<string, object> { { "commits", 30 } },
							Lang = "painless"
						}
					}
				}
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk?pipeline=default-pipeline";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Bulk(f),
			(client, f) => client.BulkAsync(f),
			(client, r) => client.Bulk(r),
			(client, r) => client.BulkAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var pipelineResponse = client.Ingest.PutPipeline("default-pipeline", p => p
				.Processors(pr => pr
					.Set<Project>(t => t.Field(f => f.Description).Value("Default"))
				)
			);
			pipelineResponse.ShouldBeValid("Failed to set up pipeline named 'default-pipeline' required for bulk {p");

			pipelineResponse = client.Ingest.PutPipeline("pipeline", p => p
				.Processors(pr => pr
					.Set<Project>(t => t.Field(f => f.Description).Value("Overridden"))
				)
			);
			pipelineResponse.ShouldBeValid($"Failed to set up pipeline named 'pipeline' required for bulk");

			base.IntegrationSetup(client, values);
		}

		protected override void ExpectResponse(BulkResponse response)
		{
			response.Took.Should().BeGreaterThan(0);
			response.Errors.Should().BeFalse();
			response.ItemsWithErrors.Should().NotBeNull().And.BeEmpty();
			response.Items.Should().NotBeEmpty();
			foreach (var item in response.Items)
			{
				item.Index.Should().Be(CallIsolatedValue);
				item.Status.Should().BeGreaterThan(100);
				item.Version.Should().BeGreaterThan(0);
				item.Id.Should().NotBeNullOrWhiteSpace();
				item.IsValid.Should().BeTrue();
				item.Shards.Should().NotBeNull();
				item.Shards.Total.Should().BeGreaterThan(0);
				item.Shards.Successful.Should().BeGreaterThan(0);
				item.SequenceNumber.Should().BeGreaterOrEqualTo(0);
				item.PrimaryTerm.Should().BeGreaterThan(0);
				item.Result.Should().NotBeNullOrEmpty();
			}

			var project1 = Client.Source<Project>(Project.Instance.Name, p => p.Index(CallIsolatedValue)).Body;
			project1.LeadDeveloper.FirstName.Should().Be("martijn");
			project1.Description.Should().Be("Overridden");

			var project2 = Client.Source<Project>(Project.Instance.Name + "2", p => p
				.Index(CallIsolatedValue)
				.Routing(Project.Instance.Name)
			).Body;
			project2.Description.Should().Be("Default");
			project2.NumberOfCommits.Should().Be(30);
		}
	}
}
