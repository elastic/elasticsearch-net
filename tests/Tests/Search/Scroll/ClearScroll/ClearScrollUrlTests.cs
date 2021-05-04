// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.Scroll.ClearScroll
{
	public class ClearScrollUrlTests
	{
		[U] public async Task Urls() => await DELETE("/_search/scroll")
			.Fluent(c => c.ClearScroll(cs => cs.ScrollId("scrollid1, scrollid2")))
			.Request(c => c.ClearScroll(new ClearScrollRequest(new[] { "scrollid1, scrollid2" })))
			.FluentAsync(c => c.ClearScrollAsync(cs => cs.ScrollId("scrollid1, scrollid2")))
			.RequestAsync(c => c.ClearScrollAsync(new ClearScrollRequest(new[] { "scrollid1, scrollid2" })));
	}
}
