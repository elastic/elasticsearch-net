using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_start")
			.Fluent(c => c.Watcher.StartWatcher())
			.Request(c => c.Watcher.StartWatcher(new StartWatcherRequest()))
			.FluentAsync(c => c.Watcher.StartWatcherAsync())
			.RequestAsync(c => c.Watcher.StartWatcherAsync(new StartWatcherRequest()));
	}
}
