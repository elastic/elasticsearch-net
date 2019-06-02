using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.ShrinkIndex
{
	public class ShrinkIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var source = "source";
			var target = "target";
			await PUT($"/{source}/_shrink/{target}")
					.Fluent(c => c.Indices.Shrink(source, target))
					.Request(c => c.Indices.Shrink(new ShrinkIndexRequest(source, target)))
					.FluentAsync(c => c.Indices.ShrinkAsync(source, target))
					.RequestAsync(c => c.Indices.ShrinkAsync(new ShrinkIndexRequest(source, target)))
				;
		}
	}
}
