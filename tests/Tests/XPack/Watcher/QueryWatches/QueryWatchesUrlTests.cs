// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.QueryWatches
{
	public class QueryWatchesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_query/watches")
			.Fluent(c => c.Watcher.QueryWatches())
			.Request(c => c.Watcher.QueryWatches(new QueryWatchesRequest()))
			.FluentAsync(c => c.Watcher.QueryWatchesAsync())
			.RequestAsync(c => c.Watcher.QueryWatchesAsync(new QueryWatchesRequest()));
	}
}
