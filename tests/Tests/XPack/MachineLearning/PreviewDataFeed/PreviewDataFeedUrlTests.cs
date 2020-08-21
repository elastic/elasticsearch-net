// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.PreviewDataFeed
{
	public class PreviewDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_ml/datafeeds/datafeed_id/_preview")
			.Fluent(c => c.MachineLearning.PreviewDatafeed<Metric>("datafeed_id", p => p))
			.Request(c => c.MachineLearning.PreviewDatafeed<Metric>(new PreviewDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.PreviewDatafeedAsync<Metric>("datafeed_id", p => p))
			.RequestAsync(c => c.MachineLearning.PreviewDatafeedAsync<Metric>(new PreviewDatafeedRequest("datafeed_id")));
	}
}
