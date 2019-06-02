using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_watcher/watch/watch_id")
			.Fluent(c => c.Watcher.Delete("watch_id"))
			.Request(c => c.Watcher.Delete(new DeleteWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.DeleteAsync("watch_id"))
			.RequestAsync(c => c.Watcher.DeleteAsync(new DeleteWatchRequest("watch_id")));
	}
}
