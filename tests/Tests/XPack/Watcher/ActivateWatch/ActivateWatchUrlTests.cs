using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.ActivateWatch
{
	public class ActivateWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id/_activate")
			.Fluent(c => c.Watcher.Activate("watch_id"))
			.Request(c => c.Watcher.Activate(new ActivateWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.ActivateAsync("watch_id"))
			.RequestAsync(c => c.Watcher.ActivateAsync(new ActivateWatchRequest("watch_id")));
	}
}
