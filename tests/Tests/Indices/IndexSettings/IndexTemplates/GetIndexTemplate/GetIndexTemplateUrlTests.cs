// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.GetIndexTemplate
{
	public class GetTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await GET($"/_template/{name}")
					.Fluent(c => c.Indices.GetTemplate(name))
					.Request(c => c.Indices.GetTemplate(new GetIndexTemplateRequest(name)))
					.FluentAsync(c => c.Indices.GetTemplateAsync(name))
					.RequestAsync(c => c.Indices.GetTemplateAsync(new GetIndexTemplateRequest(name)))
				;

			await GET($"/_template")
					.Fluent(c => c.Indices.GetTemplate())
					.Request(c => c.Indices.GetTemplate(new GetIndexTemplateRequest()))
					.FluentAsync(c => c.Indices.GetTemplateAsync())
					.RequestAsync(c => c.Indices.GetTemplateAsync(new GetIndexTemplateRequest()))
				;
		}
	}
}
