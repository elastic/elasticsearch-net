using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.ExecuteWatch
{
	public class ExecuteWatchUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/watcher/watch/watch_id/_execute")
				.Fluent(c => c.ExecuteWatch(e => e.Id("watch_id")))
				.Request(c => c.ExecuteWatch(new ExecuteWatchRequest("watch_id")))
				.FluentAsync(c => c.ExecuteWatchAsync(e => e.Id("watch_id")))
				.RequestAsync(c => c.ExecuteWatchAsync(new ExecuteWatchRequest("watch_id")))
				;
		}
	}
}
