using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Sql.TranslateSql
{
	public class TranslateSqlUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("_sql/translate")
			.Fluent(c => c.Sql.TranslateSql(d => d))
			.Request(c => c.Sql.TranslateSql(new TranslateSqlRequest()))
			.FluentAsync(c => c.Sql.TranslateSqlAsync(d => d))
			.RequestAsync(c => c.Sql.TranslateSqlAsync(new TranslateSqlRequest()));
	}
}
