using System.Threading.Tasks;
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
			await POST("/project/commits/_search_shards")
				.Fluent(c=>c.SearchShards<CommitActivity>(s=>s))
				.Request(c=>c.SearchShards(new SearchShardsRequest<CommitActivity>()))
				.FluentAsync(c=>c.SearchShardsAsync<CommitActivity>(s=>s))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<CommitActivity>()))
				;

			await POST("/project/hardcoded/_search_shards")
				.Fluent(c=>c.SearchShards<CommitActivity>(s=>s.Type(hardcoded)))
				.Request(c=>c.SearchShards(new SearchShardsRequest<CommitActivity>(typeof(Project), hardcoded)))
				.Request(c=>c.SearchShards(new SearchShardsRequest(typeof(Project), hardcoded)))
				.FluentAsync(c=>c.SearchShardsAsync<CommitActivity>(s=>s.Type(hardcoded)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<CommitActivity>(typeof(Project), hardcoded)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest(typeof(Project), hardcoded)))
				;

			await POST("/project/_search_shards")
				.Fluent(c=>c.SearchShards<Project>(s=>s.Type(Types.All)))
				.Fluent(c=>c.SearchShards<Project>(s=>s.AllTypes()))
				.Request(c=>c.SearchShards(new SearchShardsRequest("project")))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>("project", Types.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Type(Types.All)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(typeof(Project), Types.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.AllTypes()))
				;

			await POST("/hardcoded/_search_shards")
				.Fluent(c=>c.SearchShards<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.Fluent(c=>c.SearchShards<Project>(s=>s.Index(hardcoded).AllTypes()))
				.Request(c=>c.SearchShards(new SearchShardsRequest(hardcoded)))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.Index(hardcoded).AllTypes()))
				;

			await POST("/_search_shards")
				.Fluent(c=>c.SearchShards<Project>(s=>s.AllTypes().AllIndices()))
				.Request(c=>c.SearchShards(new SearchShardsRequest()))
				.Request(c=>c.SearchShards(new SearchShardsRequest<Project>(Nest.Indices.All, Types.All)))
				.FluentAsync(c=>c.SearchShardsAsync<Project>(s=>s.AllIndices().Type(Types.All)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest<Project>(Nest.Indices.All, Types.All)))
				.RequestAsync(c=>c.SearchShardsAsync(new SearchShardsRequest()))
				;
		}
	}
}
