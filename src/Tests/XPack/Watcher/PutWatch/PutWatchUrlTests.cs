using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.PutWatch
{
	public class PutWatchUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/watcher/watch/watch_id")
				.Fluent(c => c.PutWatch("watch_id"))
				.Request(c => c.PutWatch(new PutWatchRequest("watch_id")))
				.FluentAsync(c => c.PutWatchAsync("watch_id"))
				.RequestAsync(c => c.PutWatchAsync(new PutWatchRequest("watch_id")))
				;
		}
	}
}
