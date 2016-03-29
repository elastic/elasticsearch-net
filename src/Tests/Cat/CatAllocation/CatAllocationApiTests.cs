using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatAllocation
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatAllocationApiTests : ApiIntegrationTestBase<ICatResponse<CatAllocationRecord>, ICatAllocationRequest, CatAllocationDescriptor, CatAllocationRequest>
	{
		public CatAllocationApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatAllocation(),
			fluentAsync: (client, f) => client.CatAllocationAsync(),
			request: (client, r) => client.CatAllocation(r),
			requestAsync: (client, r) => client.CatAllocationAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/allocation";

		protected override void ExpectResponse(ICatResponse<CatAllocationRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Node));
		}
	}

}
