// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, CloseIndexResponse, ICloseIndexRequest, CloseIndexDescriptor, CloseIndexRequest>
	{
		public CloseIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CloseIndexRequest Initializer => new CloseIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_close";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Close(CallIsolatedValue),
			(client, f) => client.Indices.CloseAsync(CallIsolatedValue),
			(client, r) => client.Indices.Close(r),
			(client, r) => client.Indices.CloseAsync(r)
		);
	}

	[SkipVersion("<7.3.0", "individual index results only available in 7.3.0+")]
	public class CloseIndexWithShardsAcknowledgedApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, CloseIndexResponse, ICloseIndexRequest, CloseIndexDescriptor, CloseIndexRequest>
	{
		public CloseIndexWithShardsAcknowledgedApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CloseIndexRequest Initializer => new CloseIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_close";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Close(CallIsolatedValue),
			(client, f) => client.Indices.CloseAsync(CallIsolatedValue),
			(client, r) => client.Indices.Close(r),
			(client, r) => client.Indices.CloseAsync(r)
		);

		protected override void ExpectResponse(CloseIndexResponse response)
		{
			response.ShouldBeValid();
			response.ShardsAcknowledged.Should().BeTrue();
			response.Indices.Should().NotBeNull().And.ContainKey(CallIsolatedValue);

			var closeIndexResult = response.Indices[CallIsolatedValue];
			closeIndexResult.Closed.Should().BeTrue();
			closeIndexResult.Shards.Should().NotBeNull();
		}
	}
}
