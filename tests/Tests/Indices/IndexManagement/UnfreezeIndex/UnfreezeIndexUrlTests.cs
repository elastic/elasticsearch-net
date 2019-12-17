using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Nest.Indices;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.UnfreezeIndex
{
	public class UnfreezeIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "project";
			await POST($"/{index}/_unfreeze")
					.Fluent(c => c.Indices.Unfreeze(index, s => s))
					.Request(c => c.Indices.Unfreeze(new UnfreezeIndexRequest(index)))
					.FluentAsync(c => c.Indices.UnfreezeAsync(index))
					.RequestAsync(c => c.Indices.UnfreezeAsync(new UnfreezeIndexRequest(index)))
				;
		}
	}
}
