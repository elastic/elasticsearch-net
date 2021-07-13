// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Sql.Status
{
	public class SqlSearchStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_sql/async/status/search_id")
			.Fluent(c => c.Sql.SearchStatus("search_id", f => f))
			.Request(c => c.Sql.SearchStatus(new SqlSearchStatusRequest("search_id")))
			.FluentAsync(c => c.Sql.SearchStatusAsync("search_id", f => f))
			.RequestAsync(c => c.Sql.SearchStatusAsync(new SqlSearchStatusRequest("search_id")));
	}
}
