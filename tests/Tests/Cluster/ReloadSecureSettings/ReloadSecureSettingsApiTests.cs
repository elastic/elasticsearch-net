// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ReloadSecureSettings
{
	[SkipVersion("<6.5.0", "")]
	public class ReloadSecureSettingsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReloadSecureSettingsResponse, IReloadSecureSettingsRequest, ReloadSecureSettingsDescriptor, ReloadSecureSettingsRequest>
	{
		public ReloadSecureSettingsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_nodes/reload_secure_settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Nodes.ReloadSecureSettings(),
			(client, f) => client.Nodes.ReloadSecureSettingsAsync(),
			(client, r) => client.Nodes.ReloadSecureSettings(r),
			(client, r) => client.Nodes.ReloadSecureSettingsAsync(r)
		);

		protected override void ExpectResponse(ReloadSecureSettingsResponse response)
		{
			response.Nodes.Should().NotBeEmpty();
			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().BeGreaterOrEqualTo(1);
			response.NodeStatistics.Successful.Should().BeGreaterOrEqualTo(1);
			response.ClusterName.Should().NotBeNullOrWhiteSpace();

		}
	}
}
