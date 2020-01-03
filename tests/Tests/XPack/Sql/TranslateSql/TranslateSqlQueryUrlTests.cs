using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
