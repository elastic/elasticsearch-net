using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineApiTests : ApiTestBase<ReadOnlyCluster, IGetPipelineResponse, IGetPipelineRequest, GetPipelineDescriptor, GetPipelineRequest>
	{
		public GetPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private const string PipelineId = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetPipeline(f),
			fluentAsync: (client, f) => client.GetPipelineAsync(f),
			request: (client, r) => client.GetPipeline(r),
			requestAsync: (client, r) => client.GetPipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_ingest/pipeline/{PipelineId}";

		protected override GetPipelineDescriptor NewDescriptor() => new GetPipelineDescriptor().Id(PipelineId);

		protected override Func<GetPipelineDescriptor, IGetPipelineRequest> Fluent => d => d.Id(PipelineId);

		protected override GetPipelineRequest Initializer => new GetPipelineRequest(PipelineId);
	}
}
