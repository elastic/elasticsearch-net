using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await POST($"/{index}/_close")
					.Fluent(c => c.Indices.CloseIndex(indices, s => s))
					.Request(c => c.Indices.CloseIndex(new CloseIndexRequest(indices)))
					.FluentAsync(c => c.Indices.CloseIndexAsync(indices))
					.RequestAsync(c => c.Indices.CloseIndexAsync(new CloseIndexRequest(indices)))
				;
		}
	}
}
