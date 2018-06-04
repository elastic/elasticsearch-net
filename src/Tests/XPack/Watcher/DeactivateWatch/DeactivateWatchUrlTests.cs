using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.DeactivateWatch
{
	public class DeactivateWatchUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/watcher/watch/watch_id/_deactivate")
				.Fluent(c => c.DeactivateWatch("watch_id"))
				.Request(c => c.DeactivateWatch(new DeactivateWatchRequest("watch_id")))
				.FluentAsync(c => c.DeactivateWatchAsync("watch_id"))
				.RequestAsync(c => c.DeactivateWatchAsync(new DeactivateWatchRequest("watch_id")))
				;
		}
	}
}
