// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ComponentTemplate.GetComponentTemplate
{
	public class GetComponentTemplateUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_component_template/template_name")
					.Fluent(c => c.Cluster.GetComponentTemplate("template_name"))
					.Request(c => c.Cluster.GetComponentTemplate(new GetComponentTemplateRequest("template_name")))
					.FluentAsync(c => c.Cluster.GetComponentTemplateAsync("template_name"))
					.RequestAsync(c => c.Cluster.GetComponentTemplateAsync(new GetComponentTemplateRequest("template_name")))
				;

			await GET("/_component_template/template_name1%2Ctemplate_name2")
					.Fluent(c => c.Cluster.GetComponentTemplate(new[] { "template_name1", "template_name2" }))
					.Request(c => c.Cluster.GetComponentTemplate(new GetComponentTemplateRequest(new[] { "template_name1", "template_name2" })))
					.FluentAsync(c => c.Cluster.GetComponentTemplateAsync(new[] { "template_name1", "template_name2" }))
					.RequestAsync(c => c.Cluster.GetComponentTemplateAsync(new GetComponentTemplateRequest(new[] { "template_name1", "template_name2" })))
				;
		}
	}
}
