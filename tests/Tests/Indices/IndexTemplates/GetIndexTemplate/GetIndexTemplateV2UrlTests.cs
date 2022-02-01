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
	public class GetTemplateV2UrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await GET($"/_index_template/{name}")
					.Fluent(c => c.Indices.GetTemplateV2(name))
					.Request(c => c.Indices.GetTemplateV2(new GetIndexTemplateV2Request(name)))
					.FluentAsync(c => c.Indices.GetTemplateV2Async(name))
					.RequestAsync(c => c.Indices.GetTemplateV2Async(new GetIndexTemplateV2Request(name)))
				;

			await GET($"/_index_template")
					.Fluent(c => c.Indices.GetTemplateV2())
					.Request(c => c.Indices.GetTemplateV2(new GetIndexTemplateV2Request()))
					.FluentAsync(c => c.Indices.GetTemplateV2Async())
					.RequestAsync(c => c.Indices.GetTemplateV2Async(new GetIndexTemplateV2Request()))
				;
		}
	}
}
