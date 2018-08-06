using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Ingest.DeletePipeline
{
	public class DeletePipelineApiTests : ApiTestBase<ReadOnlyCluster, IDeletePipelineResponse, IDeletePipelineRequest, DeletePipelineDescriptor, DeletePipelineRequest>
	{
		public DeletePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeletePipeline(_id, f),
			fluentAsync: (client, f) => client.DeletePipelineAsync(_id, f),
			request: (client, r) => client.DeletePipeline(r),
			requestAsync: (client, r) => client.DeletePipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override DeletePipelineDescriptor NewDescriptor() => new DeletePipelineDescriptor(_id);

		protected override Func<DeletePipelineDescriptor, IDeletePipelineRequest> Fluent => d => d;

		protected override DeletePipelineRequest Initializer => new DeletePipelineRequest(_id);
	}
}
