// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.AcknowledgeWatch
{
	public class AcknowledgeWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await PUT("/_watcher/watch/watch_id/_ack")
					.Fluent(c => c.Watcher.Acknowledge("watch_id"))
					.Request(c => c.Watcher.Acknowledge(new AcknowledgeWatchRequest("watch_id")))
					.FluentAsync(c => c.Watcher.AcknowledgeAsync("watch_id"))
					.RequestAsync(c => c.Watcher.AcknowledgeAsync(new AcknowledgeWatchRequest("watch_id")))
				;

			await PUT("/_watcher/watch/watch_id/_ack/action_1%2Caction_2")
					.Fluent(c => c.Watcher.Acknowledge("watch_id", a => a.ActionId(new[] { "action_1", "action_2" })))
					.Request(c => c.Watcher.Acknowledge(new AcknowledgeWatchRequest("watch_id", new[] { "action_1", "action_2" })))
					.FluentAsync(c => c.Watcher.AcknowledgeAsync("watch_id", a => a.ActionId(new[] { "action_1", "action_2" })))
					.RequestAsync(c => c.Watcher.AcknowledgeAsync(new AcknowledgeWatchRequest("watch_id", new[] { "action_1", "action_2" })))
				;

			await PUT("/_watcher/watch/watch_id/_ack/action_1%2Caction_2")
					.Fluent(c => c.Watcher.Acknowledge("watch_id", a => a.ActionId("action_1,action_2")))
					.Request(c => c.Watcher.Acknowledge(new AcknowledgeWatchRequest("watch_id", "action_1,action_2")))
					.FluentAsync(c => c.Watcher.AcknowledgeAsync("watch_id", a => a.ActionId("action_1,action_2")))
					.RequestAsync(c => c.Watcher.AcknowledgeAsync(new AcknowledgeWatchRequest("watch_id", "action_1,action_2")))
				;
		}
	}
}
