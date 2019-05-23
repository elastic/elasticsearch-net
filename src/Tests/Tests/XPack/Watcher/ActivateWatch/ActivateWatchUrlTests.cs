using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.ActivateWatch
{
	public class ActivateWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id/_activate")
			.Fluent(c => c.Watcher.ActivateWatch("watch_id"))
			.Request(c => c.Watcher.ActivateWatch(new ActivateWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.ActivateWatchAsync("watch_id"))
			.RequestAsync(c => c.Watcher.ActivateWatchAsync(new ActivateWatchRequest("watch_id")));
	}
}
