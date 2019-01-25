using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.ReloadSecureSettings
{
	[SkipVersion("<6.5.0", "")]
	public class ReloadSecureSettingsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IReloadSecureSettingsResponse, IReloadSecureSettingsRequest, ReloadSecureSettingsDescriptor, ReloadSecureSettingsRequest>
	{
		public ReloadSecureSettingsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_nodes/reload_secure_settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReloadSecureSettings(),
			(client, f) => client.ReloadSecureSettingsAsync(),
			(client, r) => client.ReloadSecureSettings(r),
			(client, r) => client.ReloadSecureSettingsAsync(r)
		);

		protected override void ExpectResponse(IReloadSecureSettingsResponse response)
		{
			response.Nodes.Should().NotBeEmpty();
			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().BeGreaterOrEqualTo(1);
			response.NodeStatistics.Successful.Should().BeGreaterOrEqualTo(1);
			response.ClusterName.Should().NotBeNullOrWhiteSpace();

		}
	}
}
