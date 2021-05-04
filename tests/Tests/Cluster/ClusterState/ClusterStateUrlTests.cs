// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cluster/state")
					.Fluent(c => c.Cluster.State())
					.Request(c => c.Cluster.State(new ClusterStateRequest()))
					.FluentAsync(c => c.Cluster.StateAsync())
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest()))

				;

			var metrics = ClusterStateMetric.MasterNode | ClusterStateMetric.Metadata;
			await GET("/_cluster/state/metadata%2Cmaster_node")
					.Fluent(c => c.Cluster.State(null, p => p.Metric(metrics)))
					.Request(c => c.Cluster.State(new ClusterStateRequest(metrics)))
					.FluentAsync(c => c.Cluster.StateAsync(null, p => p.Metric(metrics)))
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest(metrics)))
				;

			metrics |= ClusterStateMetric.All;
			var index = "indexx";
			await GET($"/_cluster/state/_all/{index}")
					.Fluent(c => c.Cluster.State(index, p => p.Metric(metrics)))
					.Request(c => c.Cluster.State(new ClusterStateRequest(metrics, index)))
					.FluentAsync(c => c.Cluster.StateAsync(index, p => p.Metric(metrics)))
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest(metrics, index)))
				;
		}
	}
}
