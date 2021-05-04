// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.MultiSearch.MultiSearchTemplate
{
	public class MultiSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "indexx";

			await POST($"/_msearch/template")
					.Fluent(c => c.MultiSearchTemplate())
					.Request(c => c.MultiSearchTemplate(new MultiSearchTemplateRequest()))
					.FluentAsync(c => c.MultiSearchTemplateAsync())
					.RequestAsync(c => c.MultiSearchTemplateAsync(new MultiSearchTemplateRequest()))
				;

			await POST($"/{index}/_msearch/template")
					.Fluent(c => c.MultiSearchTemplate(index))
					.Request(c => c.MultiSearchTemplate(new MultiSearchTemplateRequest(index)))
					.FluentAsync(c => c.MultiSearchTemplateAsync(index))
					.RequestAsync(c => c.MultiSearchTemplateAsync(new MultiSearchTemplateRequest(index)))
				;

		}
	}
}
