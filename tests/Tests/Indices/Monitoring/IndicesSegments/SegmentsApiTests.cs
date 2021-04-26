/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Linq;
using Elasticsearch.Net;
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
