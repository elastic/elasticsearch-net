// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";

			await UrlTester.POST("/hardcoded/_search/template")
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest(hardcoded)))
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index(hardcoded)))
				;

			await UrlTester.POST("/project/_search/template")
				.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest("project")))
				.Fluent(c => c.SearchTemplate<Project>(s => s.Index("project")))
				.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(typeof(Project))))
				.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index("project")))
				.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(typeof(Project))));

			await UrlTester.POST("/_search/template")
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest()))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest()))
				;
			await UrlTester.POST("/_all/_search/template")
					.Fluent(c => c.SearchTemplate<Project>(s => s.AllIndices()))
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All)))
				;
		}
	}
}
