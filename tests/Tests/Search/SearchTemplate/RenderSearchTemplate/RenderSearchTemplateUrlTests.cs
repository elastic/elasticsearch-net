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
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.SearchTemplate.RenderSearchTemplate
{
	public class RenderSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "the-id";
			await POST("/_render/template/the-id")
					.Fluent(c => c.RenderSearchTemplate(s => s.Id(id)))
					.Request(c => c.RenderSearchTemplate(new RenderSearchTemplateRequest(id)))
					.FluentAsync(c => c.RenderSearchTemplateAsync(s => s.Id(id)))
					.RequestAsync(c => c.RenderSearchTemplateAsync(new RenderSearchTemplateRequest(id)))
				;

			await POST("/_render/template")
					.Fluent(c => c.RenderSearchTemplate(s => s.Source("")))
					.Request(c => c.RenderSearchTemplate(new RenderSearchTemplateRequest { Source = "" }))
					.FluentAsync(c => c.RenderSearchTemplateAsync(s => s.Source("")))
					.RequestAsync(c => c.RenderSearchTemplateAsync(new RenderSearchTemplateRequest { Source = "" }))
				;
		}
	}
}
