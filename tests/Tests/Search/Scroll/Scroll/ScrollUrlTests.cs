// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.Scroll.Scroll;

public class ScrollUrlTests
{
	[U] public async Task Urls() => await POST("/_search/scroll")
		.Fluent(c => c.Scroll<CommitActivity>(s => s.ScrollId("scroll_id").Scroll("1m")))
		.Request(c => c.Scroll<CommitActivity>(new ScrollRequest { ScrollId = "scroll_id", Scroll = TimeSpan.FromMinutes(1) }))
		.FluentAsync(c => c.ScrollAsync<CommitActivity>(s => s.ScrollId("scroll_id").Scroll("1m")))
		.RequestAsync(c => c.ScrollAsync<CommitActivity>(new ScrollRequest { ScrollId = "scroll_id", Scroll = TimeSpan.FromMinutes(1) }));
}
