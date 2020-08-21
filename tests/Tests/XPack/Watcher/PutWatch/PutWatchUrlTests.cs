// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.PutWatch
{
	public class PutWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id")
			.Fluent(c => c.Watcher.Put("watch_id"))
			.Request(c => c.Watcher.Put(new PutWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.PutAsync("watch_id"))
			.RequestAsync(c => c.Watcher.PutAsync(new PutWatchRequest("watch_id")));
	}
}
