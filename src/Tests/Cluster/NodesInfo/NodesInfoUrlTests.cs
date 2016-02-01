using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.NodesInfo
{
	public class NodesInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_nodes")
				.Fluent(c => c.NodesInfo())
				.Request(c => c.NodesInfo(new NodesInfoRequest()))
				.FluentAsync(c => c.NodesInfoAsync())
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest()))
				;

			await GET("/_nodes/foo")
				.Fluent(c => c.NodesInfo(n => n.NodeId("foo")))
				.Request(c => c.NodesInfo(new NodesInfoRequest("foo")))
				.FluentAsync(c => c.NodesInfoAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest("foo")))
				;

			var metrics = NodesInfoMetric.Http | NodesInfoMetric.Jvm;
			await GET("/_nodes/jvm%2Chttp")
				.Fluent(c => c.NodesInfo(p=>p.Metric(metrics)))
				.Request(c => c.NodesInfo(new NodesInfoRequest(metrics)))
				.FluentAsync(c => c.NodesInfoAsync(p=>p.Metric(metrics)))
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest(metrics)))
				;

			await GET("/_nodes/foo/jvm%2Chttp")
				.Fluent(c => c.NodesInfo(n => n.NodeId("foo").Metric(metrics)))
				.Request(c => c.NodesInfo(new NodesInfoRequest("foo", metrics)))
				.FluentAsync(c => c.NodesInfoAsync(n => n.NodeId("foo").Metric(metrics)))
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest("foo", metrics)))
				;
		}
	}
}
