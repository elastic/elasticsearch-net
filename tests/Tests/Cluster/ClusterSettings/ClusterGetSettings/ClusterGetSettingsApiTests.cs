// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch.Cluster;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterSettings.ClusterGetSettings;

public class ClusterGetSettingsApiTests
	: ApiTestBase<ReadOnlyCluster, ClusterGetSettingsResponse, ClusterGetSettingsRequestDescriptor,
		ClusterGetSettingsRequest>
{
	public ClusterGetSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override HttpMethod HttpMethod => HttpMethod.GET;
	protected override string ExpectedUrlPathAndQuery => "/_cluster/settings";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Cluster.GetSettings(),
		(client, f) => client.Cluster.GetSettingsAsync(),
		(client, r) => client.Cluster.GetSettings(r),
		(client, r) => client.Cluster.GetSettingsAsync(r)
	);
}
