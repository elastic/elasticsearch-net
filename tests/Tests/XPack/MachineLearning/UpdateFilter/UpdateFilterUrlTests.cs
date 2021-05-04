// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateFilter
{
	public class UpdateFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_ml/filters/filter_id/_update")
				.Fluent(c => c.MachineLearning.UpdateFilter("filter_id", p => p))
				.Request(c => c.MachineLearning.UpdateFilter(new UpdateFilterRequest("filter_id")))
				.FluentAsync(c => c.MachineLearning.UpdateFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.MachineLearning.UpdateFilterAsync(new UpdateFilterRequest("filter_id")));
	}
}
