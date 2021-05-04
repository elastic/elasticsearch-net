// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.Search
{
	public class SearchUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";

			await POST("/devs/_search")
					.Fluent(c => c.Search<Developer>())
					.Request(c => c.Search<Project>(new SearchRequest<Developer>()))
					.FluentAsync(c => c.SearchAsync<Developer>())
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Developer>()));

			await POST("/devs/_search")
					.Fluent(c => c.Search<Developer>(s => s))
					.Request(c => c.Search<Project>(new SearchRequest<Developer>(typeof(Developer))))
					.FluentAsync(c => c.SearchAsync<Developer>(s => s))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Developer>(typeof(Developer))));

			await POST("/project/_search")
					.Fluent(c => c.Search<Project>(s => s))
					.Fluent(c => c.Search<Project>(s => s))
					.Request(c => c.Search<Project>(new SearchRequest("project")))
					.Request(c => c.Search<Project>(new SearchRequest<Project>("project")))
					.FluentAsync(c => c.SearchAsync<Project>(s => s))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.SearchAsync<Project>(s => s));

			await POST("/hardcoded/_search")
					.Fluent(c => c.Search<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.Search<Project>(s => s.Index(hardcoded)))
					.Request(c => c.Search<Project>(new SearchRequest(hardcoded)))
					.Request(c => c.Search<Project>(new SearchRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.Index(hardcoded)));

			await POST("/_all/_search")
					.Fluent(c => c.Search<Project>(s => s.AllIndices()))
					.Request(c => c.Search<Project>(new SearchRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(Nest.Indices.All)));

			await POST("/_search?scroll=1m")
					.Request(c => c.Search<Project>(new SearchRequest { Scroll = TimeSpan.FromMinutes(1) }))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest { Scroll = 60000 }));
		}
	}
}
