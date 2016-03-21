using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatHelp
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatHelpApiTests : ApiIntegrationTestBase<ICatResponse<CatHelpRecord>, ICatHelpRequest, CatHelpDescriptor, CatHelpRequest>
	{
		public CatHelpApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatHelp(),
			fluentAsync: (client, f) => client.CatHelpAsync(),
			request: (client, r) => client.CatHelp(r),
			requestAsync: (client, r) => client.CatHelpAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat";

		protected override void ExpectResponse(ICatResponse<CatHelpRecord> response)
		{
			response.Records.Should().NotBeEmpty()
				.And.Contain(a => a.Endpoint == "/_cat/shards")
				.And.NotContain(a=>a.Endpoint == "=^.^=");
		}
	}
}
