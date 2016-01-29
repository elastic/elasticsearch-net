using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/state")
				.Fluent(c => c.ClusterState())
				.Request(c => c.ClusterState(new ClusterStateRequest()))
				.FluentAsync(c => c.ClusterStateAsync())
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest()))
				;

			var metrics = ClusterStateMetric.MasterNode | ClusterStateMetric.Metadata;
			await GET("/_cluster/state/metadata%2Cmaster_node")
				.Fluent(c => c.ClusterState(p=>p.Metric(metrics)))
				.Request(c => c.ClusterState(new ClusterStateRequest(metrics)))
				.FluentAsync(c => c.ClusterStateAsync(p=>p.Metric(metrics)))
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest(metrics)))
				;

			metrics |= ClusterStateMetric.All;
			var index = "indexx";
			await GET($"/_cluster/state/_all/{index}")
				.Fluent(c => c.ClusterState(p=>p.Metric(metrics).Index(index)))
				.Request(c => c.ClusterState(new ClusterStateRequest(metrics, index)))
				.FluentAsync(c => c.ClusterStateAsync(p=>p.Metric(metrics).Index(index)))
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest(metrics, index)))
				;
		}
	}
}
