// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Sql.Delete
{
	public class SqlDeleteUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_sql/async/delete/search_id")
			.Fluent(c => c.Sql.Delete("search_id", f => f))
			.Request(c => c.Sql.Delete(new SqlDeleteRequest("search_id")))
			.FluentAsync(c => c.Sql.DeleteAsync("search_id", f => f))
			.RequestAsync(c => c.Sql.DeleteAsync(new SqlDeleteRequest("search_id")));
	}
}
