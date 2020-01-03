using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Nest.Indices;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.FreezeIndex
{
	public class FreezeIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "project";
			await POST($"/{index}/_freeze")
					.Fluent(c => c.Indices.Freeze(index, s => s))
					.Request(c => c.Indices.Freeze(new FreezeIndexRequest(index)))
					.FluentAsync(c => c.Indices.FreezeAsync(index))
					.RequestAsync(c => c.Indices.FreezeAsync(new FreezeIndexRequest(index)))
				;
		}
	}
}
