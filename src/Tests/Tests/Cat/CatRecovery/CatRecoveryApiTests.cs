using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatRecovery
{
	public class CatRecoveryApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatRecoveryRecord>, ICatRecoveryRequest, CatRecoveryDescriptor, CatRecoveryRequest>
	{
		public CatRecoveryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/recovery";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatRecovery(),
			(client, f) => client.CatRecoveryAsync(),
			(client, r) => client.CatRecovery(r),
			(client, r) => client.CatRecoveryAsync(r)
		);
	}
}
