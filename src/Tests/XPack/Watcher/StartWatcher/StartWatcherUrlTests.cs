using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/watcher/_start")
				.Fluent(c => c.StartWatcher())
				.Request(c => c.StartWatcher(new StartWatcherRequest()))
				.FluentAsync(c => c.StartWatcherAsync())
				.RequestAsync(c => c.StartWatcherAsync(new StartWatcherRequest()))
				;
		}
	}
}
