using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatHelp
{
	public class CatHelpApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatHelpRecord>, ICatHelpRequest, CatHelpDescriptor, CatHelpRequest>
	{
		public CatHelpApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatHelp(),
			(client, f) => client.CatHelpAsync(),
			(client, r) => client.CatHelp(r),
			(client, r) => client.CatHelpAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatHelpRecord> response) => response.Records.Should()
			.NotBeEmpty()
			.And.Contain(a => a.Endpoint == "/_cat/shards/{index}")
			.And.NotContain(a => a.Endpoint == "=^.^=");
	}
}
