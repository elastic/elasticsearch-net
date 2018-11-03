using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Ingest.DeletePipeline
{
	//integration test is part of PipelineCrudTests
	public class DeletePipelineApiTests
		: ApiTestBase<ReadOnlyCluster, IDeletePipelineResponse, IDeletePipelineRequest, DeletePipelineDescriptor, DeletePipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public DeletePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeletePipelineDescriptor, IDeletePipelineRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeletePipelineRequest Initializer => new DeletePipelineRequest(_id);
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeletePipeline(_id, f),
			(client, f) => client.DeletePipelineAsync(_id, f),
			(client, r) => client.DeletePipeline(r),
			(client, r) => client.DeletePipelineAsync(r)
		);

		protected override DeletePipelineDescriptor NewDescriptor() => new DeletePipelineDescriptor(_id);
	}
}
