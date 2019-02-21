using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Sql.TranslateSql
{
	public class TranslateSqlUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("_sql/translate")
			.Fluent(c => c.TranslateSql(d => d))
			.Request(c => c.TranslateSql(new TranslateSqlRequest()))
			.FluentAsync(c => c.TranslateSqlAsync(d => d))
			.RequestAsync(c => c.TranslateSqlAsync(new TranslateSqlRequest()));
	}
}
