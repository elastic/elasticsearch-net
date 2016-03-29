using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.ClusterPendingTasks
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterPendingTasksApiTests : ApiIntegrationTestBase<IClusterPendingTasksResponse, IClusterPendingTasksRequest, ClusterPendingTasksDescriptor, ClusterPendingTasksRequest>
	{
		public ClusterPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterPendingTasks(),
			fluentAsync: (client, f) => client.ClusterPendingTasksAsync(),
			request: (client, r) => client.ClusterPendingTasks(r),
			requestAsync: (client, r) => client.ClusterPendingTasksAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/pending_tasks";

		protected override void ExpectResponse(IClusterPendingTasksResponse response)
		{
			response.Tasks.Should().NotBeNull(); 
		}
	}

}
