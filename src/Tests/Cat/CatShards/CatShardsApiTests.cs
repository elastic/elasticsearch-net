using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatShards
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatShardsApiTests : ApiIntegrationTestBase<ICatResponse<CatShardsRecord>, ICatShardsRequest, CatShardsDescriptor, CatShardsRequest>
	{
		public CatShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatShards(),
			fluentAsync: (client, f) => client.CatShardsAsync(),
			request: (client, r) => client.CatShards(r),
			requestAsync: (client, r) => client.CatShardsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/shards";

		protected override void ExpectResponse(ICatResponse<CatShardsRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.PrimaryOrReplica));
		}
	}

}
