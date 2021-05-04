// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Sql.TranslateSql
{
	public class TranslateSqlUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("_sql/translate")
			.Fluent(c => c.Sql.Translate(d => d))
			.Request(c => c.Sql.Translate(new TranslateSqlRequest()))
			.FluentAsync(c => c.Sql.TranslateAsync(d => d))
			.RequestAsync(c => c.Sql.TranslateAsync(new TranslateSqlRequest()));
	}
}
