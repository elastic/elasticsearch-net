// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Sql.ClearSqlCursor
{
	public class ClearSqlCursorUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("_sql/close")
			.Fluent(c => c.Sql.ClearCursor(d => d))
			.Request(c => c.Sql.ClearCursor(new ClearSqlCursorRequest()))
			.FluentAsync(c => c.Sql.ClearCursorAsync(d => d))
			.RequestAsync(c => c.Sql.ClearCursorAsync(new ClearSqlCursorRequest()));
	}
}
