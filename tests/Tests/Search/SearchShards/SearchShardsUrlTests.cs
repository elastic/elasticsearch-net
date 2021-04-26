/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
