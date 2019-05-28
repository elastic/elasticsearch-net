using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.DeactivateWatch
{
	public class DeactivateWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id/_deactivate")
			.Fluent(c => c.Watcher.DeactivateWatch("watch_id"))
			.Request(c => c.Watcher.DeactivateWatch(new DeactivateWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.DeactivateWatchAsync("watch_id"))
			.RequestAsync(c => c.Watcher.DeactivateWatchAsync(new DeactivateWatchRequest("watch_id")));
	}
}
