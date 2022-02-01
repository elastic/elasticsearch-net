// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutTemplateV2UrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await PUT($"/_index_template/{name}")
					.Fluent(c => c.Indices.PutTemplateV2(name, p => p))
					.Request(c => c.Indices.PutTemplateV2(new PutIndexTemplateV2Request(name)))
					.FluentAsync(c => c.Indices.PutTemplateV2Async(name, p => p))
					.RequestAsync(c => c.Indices.PutTemplateV2Async(new PutIndexTemplateV2Request(name)))
				;
		}
	}
}
