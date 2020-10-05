// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterPendingTasks
{
	public class ClusterPendingTasksApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterPendingTasksResponse, IClusterPendingTasksRequest, ClusterPendingTasksDescriptor,
			ClusterPendingTasksRequest>
	{
		public ClusterPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/pending_tasks";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.PendingTasks(),
			(client, f) => client.Cluster.PendingTasksAsync(),
			(client, r) => client.Cluster.PendingTasks(r),
			(client, r) => client.Cluster.PendingTasksAsync(r)
		);

		protected override void ExpectResponse(ClusterPendingTasksResponse response) => response.Tasks.Should().NotBeNull();
	}
}
