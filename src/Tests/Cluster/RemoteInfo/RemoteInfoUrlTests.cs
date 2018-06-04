using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.RemoteInfo
{
	public class RemoteInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_remote/info")
				.Fluent(c => c.RemoteInfo())
				.Request(c => c.RemoteInfo(new RemoteInfoRequest()))
				.FluentAsync(c => c.RemoteInfoAsync())
				.RequestAsync(c => c.RemoteInfoAsync(new RemoteInfoRequest()))
				;

		}
	}
}
