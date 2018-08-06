using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.ShrinkIndex
{
	public class ShrinkIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var source = "source";
			var target = "target";
			await PUT($"/{source}/_shrink/{target}")
				.Fluent(c => c.ShrinkIndex(source, target))
				.Request(c => c.ShrinkIndex(new ShrinkIndexRequest(source, target)))
				.FluentAsync(c => c.ShrinkIndexAsync(source, target))
				.RequestAsync(c => c.ShrinkIndexAsync(new ShrinkIndexRequest(source, target)))
				;
		}
	}
}
