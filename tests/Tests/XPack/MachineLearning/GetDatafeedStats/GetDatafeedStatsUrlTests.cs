// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/datafeeds/_stats")
					.Fluent(c => c.MachineLearning.GetDatafeedStats())
					.Request(c => c.MachineLearning.GetDatafeedStats(new GetDatafeedStatsRequest()))
					.FluentAsync(c => c.MachineLearning.GetDatafeedStatsAsync())
					.RequestAsync(c => c.MachineLearning.GetDatafeedStatsAsync(new GetDatafeedStatsRequest()))
				;

			await GET("/_ml/datafeeds/datafeed_id/_stats")
					.Fluent(c => c.MachineLearning.GetDatafeedStats(r => r.DatafeedId("datafeed_id")))
					.Request(c => c.MachineLearning.GetDatafeedStats(new GetDatafeedStatsRequest("datafeed_id")))
					.FluentAsync(c => c.MachineLearning.GetDatafeedStatsAsync(r => r.DatafeedId("datafeed_id")))
					.RequestAsync(c => c.MachineLearning.GetDatafeedStatsAsync(new GetDatafeedStatsRequest("datafeed_id")))
				;
		}
	}
}
