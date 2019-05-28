using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_stop")
			.Fluent(c => c.Watcher.StopWatcher())
			.Request(c => c.Watcher.StopWatcher(new StopWatcherRequest()))
			.FluentAsync(c => c.Watcher.StopWatcherAsync())
			.RequestAsync(c => c.Watcher.StopWatcherAsync(new StopWatcherRequest()));
	}
}
