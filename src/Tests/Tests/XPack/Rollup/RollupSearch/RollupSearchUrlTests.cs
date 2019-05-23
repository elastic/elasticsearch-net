using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.RollupSearch
{
	public class RollupSearchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string index = "default-index";
			await POST($"/{index}/_rollup_search")
				.Fluent(c => c.Rollup.Search<Log>(index, s => s))
				.Request(c => c.Rollup.Search<Log>(new RollupSearchRequest(index)))
				.FluentAsync(c => c.Rollup.SearchAsync<Log>(index, s => s))
				.RequestAsync(c => c.Rollup.SearchAsync<Log>(new RollupSearchRequest(index)));

			await POST($"/_all/_rollup_search")
				.Fluent(c => c.Rollup.Search<Log>(Nest.Indices.All, s => s))
				.Request(c => c.Rollup.Search<Log>(new RollupSearchRequest(Nest.Indices.All)))
				.FluentAsync(c => c.Rollup.SearchAsync<Log>(Nest.Indices.All, s => s))
				.RequestAsync(c => c.Rollup.SearchAsync<Log>(new RollupSearchRequest(Nest.Indices.All)));
		}
	}
}
