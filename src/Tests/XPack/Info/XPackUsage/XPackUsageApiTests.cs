using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Info.XPackUsage
{
	[SkipVersion("<5.4.0", "")]
	public class XPackUsageApiTests : ApiIntegrationTestBase<XPackCluster, IXPackUsageResponse, IXPackUsageRequest, XPackUsageDescriptor, XPackUsageRequest>
	{
		public XPackUsageApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.XPackUsage(f),
			fluentAsync: (client, f) => client.XPackUsageAsync(f),
			request: (client, r) => client.XPackUsage(r),
			requestAsync: (client, r) => client.XPackUsageAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/usage";

		protected override bool SupportsDeserialization => true;

		protected override XPackUsageRequest Initializer => new XPackUsageRequest();

		protected override void ExpectResponse(IXPackUsageResponse response)
		{
		}
	}
}
