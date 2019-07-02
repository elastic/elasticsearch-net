using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	public class OpenIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await UrlTester.POST($"/{index}/_open")
					.Fluent(c => c.Indices.Open(indices, s => s))
					.Request(c => c.Indices.Open(new OpenIndexRequest(indices)))
					.FluentAsync(c => c.Indices.OpenAsync(indices))
					.RequestAsync(c => c.Indices.OpenAsync(new OpenIndexRequest(indices)))
				;
		}
	}
}
