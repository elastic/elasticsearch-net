using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_stop")
			.Fluent(c => c.Watcher.Stop())
			.Request(c => c.Watcher.Stop(new StopWatcherRequest()))
			.FluentAsync(c => c.Watcher.StopAsync())
			.RequestAsync(c => c.Watcher.StopAsync(new StopWatcherRequest()));
	}
}
