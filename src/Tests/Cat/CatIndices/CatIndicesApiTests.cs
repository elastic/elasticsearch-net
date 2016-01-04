using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatIndices
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatIndicesApiTests : ApiIntegrationTestBase<ICatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatIndices(),
			fluentAsync: (client, f) => client.CatIndicesAsync(),
			request: (client, r) => client.CatIndices(r),
			requestAsync: (client, r) => client.CatIndicesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/indices";

		protected override void ExpectResponse(ICatResponse<CatIndicesRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
		}
	}
}
