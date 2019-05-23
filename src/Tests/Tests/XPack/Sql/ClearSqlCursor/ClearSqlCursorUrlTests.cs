using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Sql.ClearSqlCursor
{
	public class ClearSqlCursorUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("_sql/close")
			.Fluent(c => c.Sql.ClearSqlCursor(d => d))
			.Request(c => c.Sql.ClearSqlCursor(new ClearSqlCursorRequest()))
			.FluentAsync(c => c.Sql.ClearSqlCursorAsync(d => d))
			.RequestAsync(c => c.Sql.ClearSqlCursorAsync(new ClearSqlCursorRequest()));
	}
}
