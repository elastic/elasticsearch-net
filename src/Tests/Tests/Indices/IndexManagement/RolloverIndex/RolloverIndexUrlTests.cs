using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.RolloverIndex
{
	public class RolloverIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var alias = "alias1";
			await POST($"/{alias}/_rollover")
				.Fluent(c => c.Indices.RolloverIndex(alias))
				.Request(c => c.Indices.RolloverIndex(new RolloverIndexRequest(alias)))
				.FluentAsync(c => c.Indices.RolloverIndexAsync(alias))
				.RequestAsync(C => C.Indices.RolloverIndexAsync(new RolloverIndexRequest(alias)));

			var index = "newindex";

			await POST($"/{alias}/_rollover/{index}")
				.Fluent(c => c.Indices.RolloverIndex(alias, r => r.NewIndex(index)))
				.Request(c => c.Indices.RolloverIndex(new RolloverIndexRequest(alias, index)))
				.FluentAsync(c => c.Indices.RolloverIndexAsync(alias, r => r.NewIndex(index)))
				.RequestAsync(C => C.Indices.RolloverIndexAsync(new RolloverIndexRequest(alias, index)));
		}
	}
}
