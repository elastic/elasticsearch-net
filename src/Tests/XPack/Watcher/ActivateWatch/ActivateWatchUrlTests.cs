using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.ActivateWatch
{
	public class ActivateWatchUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/watcher/watch/watch_id/_activate")
				.Fluent(c => c.ActivateWatch("watch_id"))
				.Request(c => c.ActivateWatch(new ActivateWatchRequest("watch_id")))
				.FluentAsync(c => c.ActivateWatchAsync("watch_id"))
				.RequestAsync(c => c.ActivateWatchAsync(new ActivateWatchRequest("watch_id")))
				;
		}
	}
}
