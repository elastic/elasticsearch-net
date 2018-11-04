using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatSegments
{
	public class CatSegmentsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatSegmentsRecord>, ICatSegmentsRequest, CatSegmentsDescriptor, CatSegmentsRequest>
	{
		public CatSegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/segments";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatSegments(),
			(client, f) => client.CatSegmentsAsync(),
			(client, r) => client.CatSegments(r),
			(client, r) => client.CatSegmentsAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatSegmentsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Index));
	}
}
