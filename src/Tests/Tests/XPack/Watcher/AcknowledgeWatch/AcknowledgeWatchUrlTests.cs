using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.AcknowledgeWatch
{
	public class AcknowledgeWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await PUT("/_watcher/watch/watch_id/_ack")
					.Fluent(c => c.Watcher.AcknowledgeWatch("watch_id"))
					.Request(c => c.Watcher.AcknowledgeWatch(new AcknowledgeWatchRequest("watch_id")))
					.FluentAsync(c => c.Watcher.AcknowledgeWatchAsync("watch_id"))
					.RequestAsync(c => c.Watcher.AcknowledgeWatchAsync(new AcknowledgeWatchRequest("watch_id")))
				;

			await PUT("/_watcher/watch/watch_id/_ack/action_1%2Caction_2")
					.Fluent(c => c.Watcher.AcknowledgeWatch("watch_id", a => a.ActionId(new[] { "action_1", "action_2" })))
					.Request(c => c.Watcher.AcknowledgeWatch(new AcknowledgeWatchRequest("watch_id", new[] { "action_1", "action_2" })))
					.FluentAsync(c => c.Watcher.AcknowledgeWatchAsync("watch_id", a => a.ActionId(new[] { "action_1", "action_2" })))
					.RequestAsync(c => c.Watcher.AcknowledgeWatchAsync(new AcknowledgeWatchRequest("watch_id", new[] { "action_1", "action_2" })))
				;

			await PUT("/_watcher/watch/watch_id/_ack/action_1%2Caction_2")
					.Fluent(c => c.Watcher.AcknowledgeWatch("watch_id", a => a.ActionId("action_1,action_2")))
					.Request(c => c.Watcher.AcknowledgeWatch(new AcknowledgeWatchRequest("watch_id", "action_1,action_2")))
					.FluentAsync(c => c.Watcher.AcknowledgeWatchAsync("watch_id", a => a.ActionId("action_1,action_2")))
					.RequestAsync(c => c.Watcher.AcknowledgeWatchAsync(new AcknowledgeWatchRequest("watch_id", "action_1,action_2")))
				;
		}
	}
}
