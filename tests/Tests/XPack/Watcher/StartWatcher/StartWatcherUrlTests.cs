// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_watcher/_start")
			.Fluent(c => c.Watcher.Start())
			.Request(c => c.Watcher.Start(new StartWatcherRequest()))
			.FluentAsync(c => c.Watcher.StartAsync())
			.RequestAsync(c => c.Watcher.StartAsync(new StartWatcherRequest()));
	}
}
