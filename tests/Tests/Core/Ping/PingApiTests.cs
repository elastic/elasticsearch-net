// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.Core.Ping
{
	public class PingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, PingResponse, PingRequestDescriptor, PingRequest>
	{
		public PingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod ExpectHttpMethod => HttpMethod.HEAD;
		protected override string ExpectedUrlPathAndQuery => "/";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ping(),
			(client, f) => client.PingAsync(),
			(client, r) => client.Ping(r),
			(client, r) => client.PingAsync(r)
		);
	}
}
