using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	public class OpenIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await UrlTester.POST($"/{index}/_open")
					.Fluent(c => c.Indices.OpenIndex(indices, s => s))
					.Request(c => c.Indices.OpenIndex(new OpenIndexRequest(indices)))
					.FluentAsync(c => c.Indices.OpenIndexAsync(indices))
					.RequestAsync(c => c.Indices.OpenIndexAsync(new OpenIndexRequest(indices)))
				;
		}
	}
}
