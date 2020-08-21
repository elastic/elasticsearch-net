// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Nest.Infer;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.GetAlias
{
	public class GetAliasUrlTests
	{
		[U] public async Task Urls()
		{
			Name name = "hardcoded";
			IndexName index = "index";
			await GET($"/_alias")
					.Fluent(c => c.Indices.GetAlias())
					.Request(c => c.Indices.GetAlias(new GetAliasRequest()))
					.FluentAsync(c => c.Indices.GetAliasAsync())
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest()))
				;
			await GET($"/_all/_alias/hardcoded")
				.Fluent(c => c.Indices.GetAlias(AllIndices, b => b.Name(name)))
				.Request(c => c.Indices.GetAlias(new GetAliasRequest(AllIndices, name)))
				.FluentAsync(c => c.Indices.GetAliasAsync(AllIndices, b => b.Name(name)))
				.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(AllIndices, name)));

			await GET($"/_alias/hardcoded")
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(name)))
				;
			await GET($"/index/_alias")
					.Fluent(c => c.Indices.GetAlias(index))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index)))
					.FluentAsync(c => c.Indices.GetAliasAsync(index))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index)))
				;

			await GET($"/index/_alias/hardcoded")
					.Fluent(c => c.Indices.GetAlias(index, b => b.Name(name)))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index, name)))
					.FluentAsync(c => c.Indices.GetAliasAsync(index, b => b.Name(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index, name)))
				;
		}
	}
}
