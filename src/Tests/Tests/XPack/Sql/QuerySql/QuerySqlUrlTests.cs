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
			.Fluent(c => c.Sql.Query(d => d))
			.Request(c => c.Sql.Query(new QuerySqlRequest()))
			.FluentAsync(c => c.Sql.QueryAsync(d => d))
			.RequestAsync(c => c.Sql.QueryAsync(new QuerySqlRequest()));
	}
}
