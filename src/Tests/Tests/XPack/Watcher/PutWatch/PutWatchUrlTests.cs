using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.PutWatch
{
	public class PutWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id")
			.Fluent(c => c.Watcher.PutWatch("watch_id"))
			.Request(c => c.Watcher.PutWatch(new PutWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.PutWatchAsync("watch_id"))
			.RequestAsync(c => c.Watcher.PutWatchAsync(new PutWatchRequest("watch_id")));
	}
}
