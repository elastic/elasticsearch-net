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
					.Fluent(c => c.Indices.SplitIndex(source, target))
					.Request(c => c.Indices.SplitIndex(new SplitIndexRequest(source, target)))
					.FluentAsync(c => c.Indices.SplitIndexAsync(source, target))
					.RequestAsync(c => c.Indices.SplitIndexAsync(new SplitIndexRequest(source, target)))
				;
		}
	}
}
