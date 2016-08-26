using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineApiTests : ApiTestBase<ReadOnlyCluster, IGetPipelineResponse, IGetPipelineRequest, GetPipelineDescriptor, GetPipelineRequest>
	{
		public GetPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetPipeline(f),
			fluentAsync: (client, f) => client.GetPipelineAsync(f),
			request: (client, r) => client.GetPipeline(r),
			requestAsync: (client, r) => client.GetPipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override GetPipelineDescriptor NewDescriptor() => new GetPipelineDescriptor().Id(_id);

		protected override Func<GetPipelineDescriptor, IGetPipelineRequest> Fluent => d => d.Id(_id);

		protected override GetPipelineRequest Initializer => new GetPipelineRequest(_id);
	}
}
