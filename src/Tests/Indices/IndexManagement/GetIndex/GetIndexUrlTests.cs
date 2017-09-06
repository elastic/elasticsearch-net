using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await GET($"/{index}")
				.Fluent(c => c.GetIndex(index, s=>s))
				.Request(c => c.GetIndex(new GetIndexRequest(index)))
				.FluentAsync(c => c.GetIndexAsync(index))
				.RequestAsync(c => c.GetIndexAsync(new GetIndexRequest(index)))
				;
		}
	}
}
