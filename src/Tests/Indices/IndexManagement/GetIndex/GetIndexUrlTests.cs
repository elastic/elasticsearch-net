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

			var features = Feature.Settings | Feature.Mappings;
			await GET($"/{index}/_settings%2C_mappings")
				.Fluent(c => c.GetIndex(index, s=>s.Feature(features)))
				.Request(c => c.GetIndex(new GetIndexRequest(index, features)))
				.FluentAsync(c => c.GetIndexAsync(index, s=>s.Feature(features)))
				.RequestAsync(c => c.GetIndexAsync(new GetIndexRequest(index, features)))
				;
		}
	}
}
