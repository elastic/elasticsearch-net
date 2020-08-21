// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatTemplates
{
	public class CatTemplatesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/templates")
					.Fluent(c => c.Cat.Templates())
					.Request(c => c.Cat.Templates(new CatTemplatesRequest()))
					.FluentAsync(c => c.Cat.TemplatesAsync())
					.RequestAsync(c => c.Cat.TemplatesAsync(new CatTemplatesRequest()))
				;

			await GET("/_cat/templates/index-%2A")
				.Fluent(c => c.Cat.Templates(r => r.Name("index-*")))
				.Request(c => c.Cat.Templates(new CatTemplatesRequest("index-*")))
				.FluentAsync(c => c.Cat.TemplatesAsync(r => r.Name("index-*")))
				.RequestAsync(c => c.Cat.TemplatesAsync(new CatTemplatesRequest("index-*")));
		}
	}
}
