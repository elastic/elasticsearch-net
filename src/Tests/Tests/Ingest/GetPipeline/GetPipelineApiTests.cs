using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineApiTests
		: ApiTestBase<ReadOnlyCluster, IGetPipelineResponse, IGetPipelineRequest, GetPipelineDescriptor, GetPipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public GetPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetPipelineDescriptor, IGetPipelineRequest> Fluent => d => d.Id(_id);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetPipelineRequest Initializer => new GetPipelineRequest(_id);
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetPipeline(f),
			(client, f) => client.GetPipelineAsync(f),
			(client, r) => client.GetPipeline(r),
			(client, r) => client.GetPipelineAsync(r)
		);

		protected override GetPipelineDescriptor NewDescriptor() => new GetPipelineDescriptor().Id(_id);
	}
}
