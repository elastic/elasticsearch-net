using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Index
{
	public class IndexIngestApiTests :
		ApiIntegrationTestBase<IntrusiveOperationCluster, IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		private static string PipelineId { get; } = "pipeline-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		public IndexIngestApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.PutPipeline(new PutPipelineRequest(PipelineId)
			{
				Description = "Index pipeline test",
				Processors = new List<IProcessor>
				{
					new RenameProcessor
					{
						TargetField = "lastSeen",
						Field = "lastActivity"
					}
				}
			});
		}

		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> {new Tag {Name = "x", Added = FixedDate}},
			SourceOnly = TestClient.Configuration.UsingCustomSourceSerializer ? new SourceOnlyObject() : null
		};

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<Project>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<Project>(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
			);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/project/doc/{CallIsolatedValue}?wait_for_active_shards=1&op_type=index&refresh=true&routing=route&pipeline={PipelineId}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				join = Document.Join,
				state = "Stable",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				numberOfContributors = 0,
				sourceOnly = Dependant(null, new { notWrittenByDefaultSerializer = "written" }),
				curatedTags = new[] {new {name = "x", added = FixedDate}},
			};

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(this.Document);

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.WaitForActiveShards("1")
			.OpType(OpType.Index)
			.Refresh(Refresh.True)
			.Pipeline(PipelineId)
			.Routing("route");

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(this.Document)
			{
				Refresh = Refresh.True,
				OpType = OpType.Index,
				WaitForActiveShards = "1",
				Routing = "route",
				Pipeline = PipelineId
			};

	}
}
