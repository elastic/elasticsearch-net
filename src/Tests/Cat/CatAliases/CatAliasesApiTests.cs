using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatAliases
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatAliasesApiTests : ApiIntegrationTestBase<ICatResponse<CatAliasesRecord>, ICatAliasesRequest, CatAliasesDescriptor, CatAliasesRequest>
	{
		public CatAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatAliases(),
			fluentAsync: (client, f) => client.CatAliasesAsync(),
			request: (client, r) => client.CatAliases(r),
			requestAsync: (client, r) => client.CatAliasesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/aliases";

		protected override void ExpectResponse(ICatResponse<CatAliasesRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Alias == "projects-alias");
		}
	}
}
