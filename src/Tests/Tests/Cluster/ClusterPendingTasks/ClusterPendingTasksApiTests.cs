using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.ClusterPendingTasks
{
	public class ClusterPendingTasksApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IClusterPendingTasksResponse, IClusterPendingTasksRequest, ClusterPendingTasksDescriptor,
			ClusterPendingTasksRequest>
	{
		public ClusterPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/pending_tasks";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterPendingTasks(),
			(client, f) => client.ClusterPendingTasksAsync(),
			(client, r) => client.ClusterPendingTasks(r),
			(client, r) => client.ClusterPendingTasksAsync(r)
		);

		protected override void ExpectResponse(IClusterPendingTasksResponse response) => response.Tasks.Should().NotBeNull();
	}
}
