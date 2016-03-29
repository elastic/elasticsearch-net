using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatHealth
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatHealthApiTests : ApiIntegrationTestBase<ICatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatHealth(),
			fluentAsync: (client, f) => client.CatHealthAsync(),
			request: (client, r) => client.CatHealth(r),
			requestAsync: (client, r) => client.CatHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/health";

		protected override void ExpectResponse(ICatResponse<CatHealthRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
		}
	}

}
