using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatRecovery
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatRecoveryApiTests : ApiIntegrationTestBase<ICatResponse<CatRecoveryRecord>, ICatRecoveryRequest, CatRecoveryDescriptor, CatRecoveryRequest>
	{
		public CatRecoveryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatRecovery(),
			fluentAsync: (client, f) => client.CatRecoveryAsync(),
			request: (client, r) => client.CatRecovery(r),
			requestAsync: (client, r) => client.CatRecoveryAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/recovery";

	}

}
