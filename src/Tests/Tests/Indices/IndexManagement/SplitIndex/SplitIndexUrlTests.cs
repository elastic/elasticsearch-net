using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.SplitIndex
{
	public class SplitIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var source = "source";
			var target = "target";
			await PUT($"/{source}/_split/{target}")
				.Fluent(c => c.SplitIndex(source, target))
				.Request(c => c.SplitIndex(new SplitIndexRequest(source, target)))
				.FluentAsync(c => c.SplitIndexAsync(source, target))
				.RequestAsync(c => c.SplitIndexAsync(new SplitIndexRequest(source, target)))
				;
		}
	}
}
