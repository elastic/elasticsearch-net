// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.IndexTemplateExists
{
	public class IndexTemplateV2UrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await HEAD($"/_index_template/{name}")
					.Fluent(c => c.Indices.TemplateV2Exists(name))
					.Request(c => c.Indices.TemplateV2Exists(new IndexTemplateV2ExistsRequest(name)))
					.FluentAsync(c => c.Indices.TemplateV2ExistsAsync(name))
					.RequestAsync(c => c.Indices.TemplateV2ExistsAsync(new IndexTemplateV2ExistsRequest(name)))
				;
		}
	}
}
