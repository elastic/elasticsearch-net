using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatFielddata
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatFielddataApiTests : ApiIntegrationTestBase<ICatResponse<CatFielddataRecord>, ICatFielddataRequest, CatFielddataDescriptor, CatFielddataRequest>
	{
		public CatFielddataApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatFielddata(),
			fluentAsync: (client, f) => client.CatFielddataAsync(),
			request: (client, r) => client.CatFielddata(r),
			requestAsync: (client, r) => client.CatFielddataAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/fielddata";

		protected override void ExpectResponse(ICatResponse<CatFielddataRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Node));
		}
	}

}
