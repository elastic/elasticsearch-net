// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ComponentTemplate.ComponentTemplateExists
{
	public class ComponentTemplateExistsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await HEAD("/_component_template/template_name")
				.Fluent(c => c.Cluster.ComponentTemplateExists("template_name", f => f))
				.Request(c => c.Cluster.ComponentTemplateExists(new ComponentTemplateExistsRequest("template_name")))
				.FluentAsync(c => c.Cluster.ComponentTemplateExistsAsync("template_name", f => f))
				.RequestAsync(c => c.Cluster.ComponentTemplateExistsAsync(new ComponentTemplateExistsRequest("template_name")));
	}
}
