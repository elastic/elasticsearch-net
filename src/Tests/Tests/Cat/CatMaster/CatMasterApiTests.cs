using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatMaster
{
	public class CatMasterApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatMasterRecord>, ICatMasterRequest, CatMasterDescriptor, CatMasterRequest>
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
