// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatNodes
{
	public class CatNodesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatNodesRecord>, ICatNodesRequest, CatNodesDescriptor, CatNodesRequest>
	{
		public CatNodesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/nodes";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Nodes(f),
			(client, f) => client.Cat.NodesAsync(f),
			(client, r) => client.Cat.Nodes(r),
			(client, r) => client.Cat.NodesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatNodesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Name));
	}
}
