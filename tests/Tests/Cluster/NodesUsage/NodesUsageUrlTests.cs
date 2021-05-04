// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.Cluster.NodesUsage
{
	public class NodesUsageUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.GET("/_nodes/usage")
					.Fluent(c => c.Nodes.Usage(d => d))
					.Request(c => c.Nodes.Usage(new NodesUsageRequest()))
					.FluentAsync(c => c.Nodes.UsageAsync(d => d))
					.RequestAsync(c => c.Nodes.UsageAsync(new NodesUsageRequest()))
				;

			await UrlTester.GET("/_nodes/nodeId/usage")
					.Fluent(c => c.Nodes.Usage(d => d.NodeId("nodeId")))
					.Request(c => c.Nodes.Usage(new NodesUsageRequest("nodeId")))
					.FluentAsync(c => c.Nodes.UsageAsync(d => d.NodeId("nodeId")))
					.RequestAsync(c => c.Nodes.UsageAsync(new NodesUsageRequest("nodeId")))
				;

			await UrlTester.GET("/_nodes/nodeId/usage/rest_actions")
					.Fluent(c => c.Nodes.Usage(d => d.NodeId("nodeId").Metric(NodesUsageMetric.RestActions)))
					.Request(c => c.Nodes.Usage(new NodesUsageRequest("nodeId", NodesUsageMetric.RestActions)))
					.FluentAsync(c => c.Nodes.UsageAsync(d => d.NodeId("nodeId").Metric(NodesUsageMetric.RestActions)))
					.RequestAsync(c => c.Nodes.UsageAsync(new NodesUsageRequest("nodeId", NodesUsageMetric.RestActions)))
				;

			await UrlTester.GET("/_nodes/nodeId/usage/_all")
					.Fluent(c => c.Nodes.Usage(d => d.NodeId("nodeId").Metric(NodesUsageMetric.All)))
					.Request(c => c.Nodes.Usage(new NodesUsageRequest("nodeId", NodesUsageMetric.All)))
					.FluentAsync(c => c.Nodes.UsageAsync(d => d.NodeId("nodeId").Metric(NodesUsageMetric.All)))
					.RequestAsync(c => c.Nodes.UsageAsync(new NodesUsageRequest("nodeId", NodesUsageMetric.All)))
				;
		}
	}
}
