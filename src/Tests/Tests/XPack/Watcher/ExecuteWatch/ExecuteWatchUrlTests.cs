using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.ExecuteWatch
{
	public class ExecuteWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id/_execute")
			.Fluent(c => c.Watcher.ExecuteWatch(e => e.Id("watch_id")))
			.Request(c => c.Watcher.ExecuteWatch(new ExecuteWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.ExecuteWatchAsync(e => e.Id("watch_id")))
			.RequestAsync(c => c.Watcher.ExecuteWatchAsync(new ExecuteWatchRequest("watch_id")));
	}
}
