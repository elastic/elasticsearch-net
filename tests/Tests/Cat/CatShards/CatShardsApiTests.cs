// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatShards
{
	public class CatShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatShardsRecord>, ICatShardsRequest, CatShardsDescriptor, CatShardsRequest>
	{
		public CatShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/shards";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Shards(),
			(client, f) => client.Cat.ShardsAsync(),
			(client, r) => client.Cat.Shards(r),
			(client, r) => client.Cat.ShardsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatShardsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.PrimaryOrReplica));
	}
}
