using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.RestartWatcher
{
	public class RestartWatcherUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/watcher/_restart")
				.Fluent(c => c.RestartWatcher())
				.Request(c => c.RestartWatcher(new RestartWatcherRequest()))
				.FluentAsync(c => c.RestartWatcherAsync())
				.RequestAsync(c => c.RestartWatcherAsync(new RestartWatcherRequest()))
				;
		}
	}
}
