using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.SearchShards
{
	public class SearchShardsUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/project/_search_shards")
				.Request(c=>c.SearchShards(new SearchShardsRequest("project")))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>(typeof(Project))))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(typeof(Project))))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Index<Project>()))
				;

			await POST("/hardcoded/_search_shards")
				.Request(c=>c.SearchShards(new SearchShardsRequest(hardcoded)))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>(hardcoded)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Index(hardcoded)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(hardcoded)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Index(hardcoded)))
				;

			await POST("/_search_shards")
				.Fluent(c=>c.SearchShards<Project>(s=>s.AllIndices()))
				.Request(c=>c.SearchShards(new SearchShardsRequest()))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>(Nest.Indices.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.AllIndices()))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(Nest.Indices.All)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest()))
				;
		}
	}
}
