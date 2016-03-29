using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatPlugins
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatPluginsApiTests : ApiIntegrationTestBase<ICatResponse<CatPluginsRecord>, ICatPluginsRequest, CatPluginsDescriptor, CatPluginsRequest>
	{
		public CatPluginsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatPlugins(),
			fluentAsync: (client, f) => client.CatPluginsAsync(),
			request: (client, r) => client.CatPlugins(r),
			requestAsync: (client, r) => client.CatPluginsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/plugins";

		protected override void ExpectResponse(ICatResponse<CatPluginsRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Name) && a.Component == "delete-by-query");
		}
	}
}
