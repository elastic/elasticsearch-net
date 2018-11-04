using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatNodeAttributes
{
	public class CatNodeAttributesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatNodeAttributesRecord>, ICatNodeAttributesRequest, CatNodeAttributesDescriptor,
			CatNodeAttributesRequest>
	{
		public CatNodeAttributesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/nodeattrs";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatNodeAttributes(),
			(client, f) => client.CatNodeAttributesAsync(),
			(client, r) => client.CatNodeAttributes(r),
			(client, r) => client.CatNodeAttributesAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatNodeAttributesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Attribute == "testingcluster");
	}
}
