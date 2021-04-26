/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
