using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await GET($"/{index}")
					.Fluent(c => c.Indices.Get(index, s => s))
					.Request(c => c.Indices.Get(new GetIndexRequest(index)))
					.FluentAsync(c => c.Indices.GetAsync(index))
					.RequestAsync(c => c.Indices.GetAsync(new GetIndexRequest(index)))
				;
		}
	}
}
