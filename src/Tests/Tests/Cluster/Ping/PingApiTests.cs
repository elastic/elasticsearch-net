using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cluster.Ping
{
	public class PingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IPingResponse, IPingRequest, PingDescriptor, PingRequest>
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
