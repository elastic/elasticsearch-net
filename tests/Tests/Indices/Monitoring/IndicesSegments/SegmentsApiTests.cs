// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	public class SegmentsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, SegmentsResponse, ISegmentsRequest, SegmentsDescriptor, SegmentsRequest>
	{
		public SegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SegmentsDescriptor, ISegmentsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override SegmentsRequest Initializer => new SegmentsRequest(Infer.AllIndices);
		protected override string UrlPath => "/_all/_segments";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Segments(Infer.AllIndices, f),
			(client, f) => client.Indices.SegmentsAsync(Infer.AllIndices, f),
			(client, r) => client.Indices.Segments(r),
			(client, r) => client.Indices.SegmentsAsync(r)
		);

		protected override void ExpectResponse(SegmentsResponse response)
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

					if (TestConfiguration.Instance.InRange(">=6.1.0"))
						segmentValue.Attributes.Should().NotBeEmpty();
				}
			}
		}
	}
}
