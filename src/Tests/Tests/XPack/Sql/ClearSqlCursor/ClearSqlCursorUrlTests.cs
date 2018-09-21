using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Graph.Explore
{
	public class ClearSqlCursorUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("_xpack/sql")
			.Fluent(c => c.ClearSqlCursor(d => d))
			.Request(c => c.ClearSqlCursor(new ClearSqlCursorRequest()))
			.FluentAsync(c => c.ClearSqlCursorAsync(d => d))
			.RequestAsync(c => c.ClearSqlCursorAsync(new ClearSqlCursorRequest()));
	}
}
