// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Eql.Status
{
	public class EqlSearchStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_eql/search/status/search_id")
			.Fluent(c => c.Eql.SearchStatus("search_id", f => f))
			.Request(c => c.Eql.SearchStatus(new EqlSearchStatusRequest("search_id")))
			.FluentAsync(c => c.Eql.SearchStatusAsync("search_id", f => f))
			.RequestAsync(c => c.Eql.SearchStatusAsync(new EqlSearchStatusRequest("search_id")));
	}
}
