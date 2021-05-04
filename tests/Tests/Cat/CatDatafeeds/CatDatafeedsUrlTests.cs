// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatDatafeeds
{
	public class CatDatafeedsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/ml/datafeeds")
				.Fluent(c => c.Cat.Datafeeds())
				.Request(c => c.Cat.Datafeeds(new CatDatafeedsRequest()))
				.FluentAsync(c => c.Cat.DatafeedsAsync())
				.RequestAsync(c => c.Cat.DatafeedsAsync(new CatDatafeedsRequest()));

			await GET("/_cat/ml/datafeeds/feed-id")
				.Fluent(c => c.Cat.Datafeeds(f => f.DatafeedId("feed-id")))
				.Request(c => c.Cat.Datafeeds(new CatDatafeedsRequest("feed-id")))
				.FluentAsync(c => c.Cat.DatafeedsAsync(f => f.DatafeedId("feed-id")))
				.RequestAsync(c => c.Cat.DatafeedsAsync(new CatDatafeedsRequest("feed-id")));
		}
	}
}
