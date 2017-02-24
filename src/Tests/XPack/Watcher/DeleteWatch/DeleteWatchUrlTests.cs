using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/watcher/watch/watch_id")
				.Fluent(c => c.DeleteWatch("watch_id"))
				.Request(c => c.DeleteWatch(new DeleteWatchRequest("watch_id")))
				.FluentAsync(c => c.DeleteWatchAsync("watch_id"))
				.RequestAsync(c => c.DeleteWatchAsync(new DeleteWatchRequest("watch_id")))
				;
		}
	}
}
