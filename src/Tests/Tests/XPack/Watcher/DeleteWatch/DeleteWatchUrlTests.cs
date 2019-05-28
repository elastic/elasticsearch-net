using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_watcher/watch/watch_id")
			.Fluent(c => c.Watcher.DeleteWatch("watch_id"))
			.Request(c => c.Watcher.DeleteWatch(new DeleteWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.DeleteWatchAsync("watch_id"))
			.RequestAsync(c => c.Watcher.DeleteWatchAsync(new DeleteWatchRequest("watch_id")));
	}
}
