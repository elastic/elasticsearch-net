// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.Scroll.Scroll
{
	public class ScrollUrlTests
	{
		[U] public async Task Urls() => await POST("/_search/scroll")
			.Fluent(c => c.Scroll<CommitActivity>("1m", "scroll_id"))
			.Request(c => c.Scroll<CommitActivity>(new ScrollRequest("scroll_id", TimeSpan.FromMinutes(1))))
			.FluentAsync(c => c.ScrollAsync<CommitActivity>("1m", "scroll_id"))
			.RequestAsync(c => c.ScrollAsync<CommitActivity>(new ScrollRequest("scroll_id", "1m")));
	}
}
