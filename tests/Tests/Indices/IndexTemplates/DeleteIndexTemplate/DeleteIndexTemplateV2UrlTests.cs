// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate
{
	public class DeleteTemplateV2UrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await DELETE($"/_index_template/{name}")
					.Fluent(c => c.Indices.DeleteTemplateV2(name))
					.Request(c => c.Indices.DeleteTemplateV2(new DeleteIndexTemplateV2Request(name)))
					.FluentAsync(c => c.Indices.DeleteTemplateV2Async(name))
					.RequestAsync(c => c.Indices.DeleteTemplateV2Async(new DeleteIndexTemplateV2Request(name)))
				;
		}
	}
}
