// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.GetWatch
{
	public class GetWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_watcher/watch/watch_id")
			.Fluent(c => c.Watcher.Get("watch_id"))
			.Request(c => c.Watcher.Get(new GetWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.GetAsync("watch_id"))
			.RequestAsync(c => c.Watcher.GetAsync(new GetWatchRequest("watch_id")));
	}
}
