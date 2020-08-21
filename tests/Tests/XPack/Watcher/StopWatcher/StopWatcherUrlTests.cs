// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_stop")
			.Fluent(c => c.Watcher.Stop())
			.Request(c => c.Watcher.Stop(new StopWatcherRequest()))
			.FluentAsync(c => c.Watcher.StopAsync())
			.RequestAsync(c => c.Watcher.StopAsync(new StopWatcherRequest()));
	}
}
