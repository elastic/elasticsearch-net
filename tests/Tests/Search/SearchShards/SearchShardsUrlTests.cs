// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.SearchShards
{
	public class SearchShardsUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/project/_search_shards")
					.Request(c => c.SearchShards(new SearchShardsRequest("project")))
					.Request(c => c.SearchShards(new SearchShardsRequest<Project>(typeof(Project))))
					.RequestAsync(c => c.SearchShardsAsync(new SearchShardsRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.SearchShardsAsync<Project>(s => s.Index<Project>()))
				;

			await POST("/hardcoded/_search_shards")
					.Request(c => c.SearchShards(new SearchShardsRequest(hardcoded)))
					.Request(c => c.SearchShards(new SearchShardsRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchShardsAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.SearchShardsAsync(new SearchShardsRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchShardsAsync<Project>(s => s.Index(hardcoded)))
				;

			await POST("/_search_shards")
					.Request(c => c.SearchShards(new SearchShardsRequest()))
					.RequestAsync(c => c.SearchShardsAsync(new SearchShardsRequest()))
				;
			await POST("/_all/_search_shards")
					.Fluent(c => c.SearchShards<Project>(s => s.AllIndices()))
					.Request(c => c.SearchShards(new SearchShardsRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SearchShardsAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SearchShardsAsync(new SearchShardsRequest<Project>(Nest.Indices.All)))
				;
		}
	}
}
