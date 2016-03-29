using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatSegments
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatSegmentsApiTests : ApiIntegrationTestBase<ICatResponse<CatSegmentsRecord>, ICatSegmentsRequest, CatSegmentsDescriptor, CatSegmentsRequest>
	{
		public CatSegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatSegments(),
			fluentAsync: (client, f) => client.CatSegmentsAsync(),
			request: (client, r) => client.CatSegments(r),
			requestAsync: (client, r) => client.CatSegmentsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/segments";

		protected override void ExpectResponse(ICatResponse<CatSegmentsRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Index));
		}
	}

}
