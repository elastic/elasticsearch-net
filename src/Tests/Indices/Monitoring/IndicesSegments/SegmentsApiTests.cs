using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SegmentsApiTests : ApiIntegrationTestBase<ISegmentsResponse, ISegmentsRequest, SegmentsDescriptor, SegmentsRequest>
	{
		public SegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Segments(Infer.AllIndices, f),
			fluentAsync: (client, f) => client.SegmentsAsync(Infer.AllIndices, f),
			request: (client, r) => client.Segments(r),
			requestAsync: (client, r) => client.SegmentsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_segments";

		protected override Func<SegmentsDescriptor, ISegmentsRequest> Fluent => d => d;

		protected override SegmentsRequest Initializer => new SegmentsRequest(Infer.AllIndices);
	}
}
