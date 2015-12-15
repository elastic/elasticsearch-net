using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.CreateIndex
{
	public class CreateIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await PUT($"/{index}")
				.Fluent(c => c.CreateIndex(index, s=>s))
				.Request(c => c.CreateIndex(new CreateIndexRequest(index)))
				.FluentAsync(c => c.CreateIndexAsync(index))
				.RequestAsync(c => c.CreateIndexAsync(new CreateIndexRequest(index)))
				;

		}
	}
}
