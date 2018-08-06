using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
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
