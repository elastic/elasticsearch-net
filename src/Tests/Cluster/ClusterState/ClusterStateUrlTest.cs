using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using Elasticsearch.Net;

namespace Tests.Cat.CatAliases
{
	public class ClusterStateUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/state")
				.Fluent(c => c.ClusterState())
				.Request(c => c.ClusterState(new ClusterStateRequest()))
				.FluentAsync(c => c.ClusterStateAsync())
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest()))
				;


			// TODO: need to implement Metric
			var metrics = ClusterStateMetric.MasterNode | ClusterStateMetric.Metadata;
			await GET("/_cluster/state/metadata,master_node")
				.Fluent(c => c.ClusterState(p=>p.Metric(metrics)))
				.Request(c => c.ClusterState(new ClusterStateRequest(metrics)))
				.FluentAsync(c => c.ClusterStateAsync(p=>p.Metric(metrics)))
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest(metrics)))
				;

			// TODO: need to implement Metric
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
