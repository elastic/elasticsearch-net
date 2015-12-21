using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.Ping
{
	[Collection(IntegrationContext.ReadOnly)]
	public class PingApiTests : ApiIntegrationTestBase<IPingResponse, IPingRequest, PingDescriptor, PingRequest>
	{
		public PingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Ping(),
			fluentAsync: (client, f) => client.PingAsync(),
			request: (client, r) => client.Ping(r),
			requestAsync: (client, r) => client.PingAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => "/";

	}

}
