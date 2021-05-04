// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.NodesInfo
{
	public class NodesInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_nodes")
					.Fluent(c => c.Nodes.Info())
					.Request(c => c.Nodes.Info(new NodesInfoRequest()))
					.FluentAsync(c => c.Nodes.InfoAsync())
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest()))
				;

			await GET("/_nodes/foo")
					.Fluent(c => c.Nodes.Info(n => n.NodeId("foo")))
					.Request(c => c.Nodes.Info(new NodesInfoRequest("foo")))
					.FluentAsync(c => c.Nodes.InfoAsync(n => n.NodeId("foo")))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest("foo")))
				;

			var metrics = NodesInfoMetric.Http | NodesInfoMetric.Jvm;
			await GET("/_nodes/jvm%2Chttp")
					.Fluent(c => c.Nodes.Info(p => p.Metric(metrics)))
					.Request(c => c.Nodes.Info(new NodesInfoRequest(metrics)))
					.FluentAsync(c => c.Nodes.InfoAsync(p => p.Metric(metrics)))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest(metrics)))
				;

			await GET("/_nodes/foo/jvm%2Chttp")
					.Fluent(c => c.Nodes.Info(n => n.NodeId("foo").Metric(metrics)))
					.Request(c => c.Nodes.Info(new NodesInfoRequest("foo", metrics)))
					.FluentAsync(c => c.Nodes.InfoAsync(n => n.NodeId("foo").Metric(metrics)))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest("foo", metrics)))
				;
		}
	}
}
