using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatAllocation
{
	public class CatAllocationApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatAllocationRecord>, ICatAllocationRequest, CatAllocationDescriptor,
			CatAllocationRequest>
	{
		public CatAllocationApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/allocation";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Allocation(),
			(client, f) => client.Cat.AllocationAsync(),
			(client, r) => client.Cat.Allocation(r),
			(client, r) => client.Cat.AllocationAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatAllocationRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Node));
	}
}
