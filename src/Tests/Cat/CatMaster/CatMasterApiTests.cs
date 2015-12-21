using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatMaster
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatMasterApiTests : ApiIntegrationTestBase<ICatResponse<CatMasterRecord>, ICatMasterRequest, CatMasterDescriptor, CatMasterRequest>
	{
		public CatMasterApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatMaster(),
			fluentAsync: (client, f) => client.CatMasterAsync(),
			request: (client, r) => client.CatMaster(r),
			requestAsync: (client, r) => client.CatMasterAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/master";

		protected override void ExpectResponse(ICatResponse<CatMasterRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Node));
		}
	}
}
