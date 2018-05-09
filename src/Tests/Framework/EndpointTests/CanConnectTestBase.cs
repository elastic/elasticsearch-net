using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class CanConnectTestBase<TCluster> :
		ApiIntegrationTestBase<TCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		protected CanConnectTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
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
