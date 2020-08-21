// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
