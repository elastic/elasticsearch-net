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
