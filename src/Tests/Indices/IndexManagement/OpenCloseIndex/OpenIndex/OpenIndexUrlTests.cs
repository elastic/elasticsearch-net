using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Indices;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.OpenOpenIndex.OpenIndex
{
	public class OpenIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project%2Ccommits";
			await POST($"/{index}/_open")
				.Fluent(c => c.OpenIndex(indices, s=>s))
				.Request(c => c.OpenIndex(new OpenIndexRequest(indices)))
				.FluentAsync(c => c.OpenIndexAsync(indices))
				.RequestAsync(c => c.OpenIndexAsync(new OpenIndexRequest(indices)))
				;

		}
	}
}
