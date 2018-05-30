using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	public class SegmentsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISegmentsResponse, ISegmentsRequest, SegmentsDescriptor, SegmentsRequest>
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

		protected override void ExpectResponse(ISegmentsResponse response)
		{
			response.ShouldBeValid();

			foreach (var indexShard in response.Indices.SelectMany(i => i.Value.Shards))
			{
				indexShard.Key.Should().NotBeNullOrEmpty();
				foreach (var segment in indexShard.Value.Segments)
				{
					segment.Key.Should().NotBeNullOrEmpty();
					var segmentValue = segment.Value;

					segmentValue.Should().NotBeNull();
					segmentValue.MemoryInBytes.Should().BeGreaterThan(0);
					segmentValue.SizeInBytes.Should().BeGreaterThan(0);
					segmentValue.Version.Should().NotBeNullOrEmpty();

					if (TestClient.Configuration.ElasticsearchVersion.InRange(">=6.1.0"))
						segmentValue.Attributes.Should().NotBeEmpty();
				}
			}
		}
	}
}
