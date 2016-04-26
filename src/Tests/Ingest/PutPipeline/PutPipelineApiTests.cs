using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Ingest.PutPipeline
{
	[Collection(IntegrationContext.ReadOnly)]
	public class PutPipelineApiTests : ApiTestBase<IPutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		public PutPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutPipeline(_id, f),
			fluentAsync: (client, f) => client.PutPipelineAsync(_id, f),
			request: (client, r) => client.PutPipeline(r),
			requestAsync: (client, r) => client.PutPipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
		};

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d;

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id);
	}
}
