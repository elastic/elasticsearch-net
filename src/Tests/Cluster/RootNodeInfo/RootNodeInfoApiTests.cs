using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.RootNodeInfo
{
	[Collection(IntegrationContext.ReadOnly)]
	public class RootNodeInfoApiTests : ApiIntegrationTestBase<IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
	{
		public RootNodeInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RootNodeInfo(),
			fluentAsync: (client, f) => client.RootNodeInfoAsync(),
			request: (client, r) => client.RootNodeInfo(r),
			requestAsync: (client, r) => client.RootNodeInfoAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/";

		protected override void ExpectResponse(IRootNodeInfoResponse response)
		{
			response.Version.Should().NotBeNull();
			response.Version.LuceneVersion.Should().NotBeNullOrWhiteSpace();
			response.Tagline.Should().NotBeNullOrWhiteSpace();
			response.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
