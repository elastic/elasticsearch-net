using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterSettings.ClusterGetSettings
{
	public class ClusterGetSettingsApiTests
		: ApiTestBase<ReadOnlyCluster, ClusterGetSettingsResponse, IClusterGetSettingsRequest, ClusterGetSettingsDescriptor,
			ClusterGetSettingsRequest>
	{
		public ClusterGetSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/settings";

		protected override ClusterGetSettingsRequest Initializer { get; } = new ClusterGetSettingsRequest();

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.GetSettings(),
			(client, f) => client.Cluster.GetSettingsAsync(),
			(client, r) => client.Cluster.GetSettings(r),
			(client, r) => client.Cluster.GetSettingsAsync(r)
		);
	}
}
