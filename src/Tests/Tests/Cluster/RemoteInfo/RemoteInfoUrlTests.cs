using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.RemoteInfo
{
	public class RemoteInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_remote/info")
			.Fluent(c => c.Cluster.RemoteInfo())
			.Request(c => c.Cluster.RemoteInfo(new RemoteInfoRequest()))
			.FluentAsync(c => c.Cluster.RemoteInfoAsync())
			.RequestAsync(c => c.Cluster.RemoteInfoAsync(new RemoteInfoRequest()));
	}
}
