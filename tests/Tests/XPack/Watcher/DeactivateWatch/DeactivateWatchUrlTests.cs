// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.DeactivateWatch
{
	public class DeactivateWatchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_watcher/watch/watch_id/_deactivate")
			.Fluent(c => c.Watcher.Deactivate("watch_id"))
			.Request(c => c.Watcher.Deactivate(new DeactivateWatchRequest("watch_id")))
			.FluentAsync(c => c.Watcher.DeactivateAsync("watch_id"))
			.RequestAsync(c => c.Watcher.DeactivateAsync(new DeactivateWatchRequest("watch_id")));
	}
}
