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
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_stats")
					.Request(c => c.Indices.Stats(new IndicesStatsRequest()))
					.RequestAsync(c => c.Indices.StatsAsync(new IndicesStatsRequest()))
				;

			await GET($"/_all/_stats")
					.Fluent(c => c.Indices.Stats(All))
					.Request(c => c.Indices.Stats(new IndicesStatsRequest(All)))
					.FluentAsync(c => c.Indices.StatsAsync(All))
					.RequestAsync(c => c.Indices.StatsAsync(new IndicesStatsRequest(All)))
				;
			var index = "index1,index2";
			await GET($"/index1%2Cindex2/_stats")
					.Fluent(c => c.Indices.Stats(index))
					.Request(c => c.Indices.Stats(new IndicesStatsRequest(index)))
					.FluentAsync(c => c.Indices.StatsAsync(index))
					.RequestAsync(c => c.Indices.StatsAsync(new IndicesStatsRequest(index)))
				;

			var metrics = IndicesStatsMetric.Completion | IndicesStatsMetric.Flush;
			await GET($"/index1%2Cindex2/_stats/flush%2Ccompletion")
					.Fluent(c => c.Indices.Stats(index, i => i.Metric(metrics)))
					.Request(c => c.Indices.Stats(new IndicesStatsRequest(index, metrics)))
					.FluentAsync(c => c.Indices.StatsAsync(index, i => i.Metric(metrics)))
					.RequestAsync(c => c.Indices.StatsAsync(new IndicesStatsRequest(index, metrics)))
				;

			metrics = IndicesStatsMetric.Completion | IndicesStatsMetric.Flush | IndicesStatsMetric.All;
			var request = new IndicesStatsRequest(index, metrics);
			await GET($"/index1%2Cindex2/_stats/_all")
					.Fluent(c => c.Indices.Stats(index, i => i.Metric(metrics)))
					.Request(c => c.Indices.Stats(request))
					.FluentAsync(c => c.Indices.StatsAsync(index, i => i.Metric(metrics)))
					.RequestAsync(c => c.Indices.StatsAsync(request))
				;
		}
	}
}
