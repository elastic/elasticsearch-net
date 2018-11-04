using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Ingest.GetPipeline
{
	//integration test is part of PipelineCrudTests
	public class GetPipelineApiTests
		: ApiTestBase<ReadOnlyCluster, IGetPipelineResponse, IGetPipelineRequest, GetPipelineDescriptor, GetPipelineRequest>
	{
		private const string PipelineId = "pipeline-1";

		public GetPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetPipelineDescriptor, IGetPipelineRequest> Fluent => d => d.Id(PipelineId);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetPipelineRequest Initializer => new GetPipelineRequest(PipelineId);
		protected override string UrlPath => $"/_ingest/pipeline/{PipelineId}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetPipeline(f),
			(client, f) => client.GetPipelineAsync(f),
			(client, r) => client.GetPipeline(r),
			(client, r) => client.GetPipelineAsync(r)
		);

		protected override GetPipelineDescriptor NewDescriptor() => new GetPipelineDescriptor().Id(PipelineId);
	}
}
