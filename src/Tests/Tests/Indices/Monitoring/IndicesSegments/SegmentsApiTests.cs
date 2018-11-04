using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	public class SegmentsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISegmentsResponse, ISegmentsRequest, SegmentsDescriptor, SegmentsRequest>
	{
		public SegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SegmentsDescriptor, ISegmentsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override SegmentsRequest Initializer => new SegmentsRequest(Infer.AllIndices);
		protected override string UrlPath => "/_segments";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Segments(Infer.AllIndices, f),
			(client, f) => client.SegmentsAsync(Infer.AllIndices, f),
			(client, r) => client.Segments(r),
			(client, r) => client.SegmentsAsync(r)
		);
	}
}
