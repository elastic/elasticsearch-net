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
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.WatcherStats
{
	public class WatcherStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_watcher/stats")
					.Fluent(c => c.Watcher.Stats())
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest()))
					.FluentAsync(c => c.Watcher.StatsAsync())
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest()))
				;

			await GET("/_watcher/stats/_all")
					.Fluent(c => c.Watcher.Stats(r => r.Metric(WatcherStatsMetric.All)))
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest(WatcherStatsMetric.All)))
					.FluentAsync(c => c.Watcher.StatsAsync(r => r.Metric(WatcherStatsMetric.All)))
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest(WatcherStatsMetric.All)))
				;

			var metrics = WatcherStatsMetric.QueuedWatches | WatcherStatsMetric.CurrentWatches;
			await GET("/_watcher/stats/queued_watches%2Ccurrent_watches")
					.Fluent(c => c.Watcher.Stats(r => r.Metric(metrics)))
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest(metrics)))
					.FluentAsync(c => c.Watcher.StatsAsync(r => r.Metric(metrics)))
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest(metrics)))
				;
		}
	}
}
