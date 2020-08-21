// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/datafeeds/datafeed_id")
			.Fluent(c => c.MachineLearning.DeleteDatafeed("datafeed_id"))
			.Request(c => c.MachineLearning.DeleteDatafeed(new DeleteDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.DeleteDatafeedAsync("datafeed_id"))
			.RequestAsync(c => c.MachineLearning.DeleteDatafeedAsync(new DeleteDatafeedRequest("datafeed_id")));
	}
}
