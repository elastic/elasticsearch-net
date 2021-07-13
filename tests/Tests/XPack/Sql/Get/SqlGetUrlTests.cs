// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Sql.Get
{
	public class SqlGetUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_sql/async/search_id")
			.Fluent(c => c.Sql.Get("search_id", f => f))
			.Request(c => c.Sql.Get(new SqlGetRequest("search_id")))
			.FluentAsync(c => c.Sql.GetAsync("search_id", f => f))
			.RequestAsync(c => c.Sql.GetAsync(new SqlGetRequest("search_id")));
	}
}
