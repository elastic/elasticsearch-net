using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Graph.Explore
{
	public class QuerySqlUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("_sql")
			.Fluent(c => c.QuerySql(d => d))
			.Request(c => c.QuerySql(new QuerySqlRequest()))
			.FluentAsync(c => c.QuerySqlAsync(d => d))
			.RequestAsync(c => c.QuerySqlAsync(new QuerySqlRequest()));
	}
}
