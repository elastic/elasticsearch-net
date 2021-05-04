// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.AliasExists
{
	public class AliasExistsUrlTests
	{
		[U] public async Task Urls()
		{
			Name name = "hardcoded";
			IndexName index = "index";
			await HEAD($"/_alias/hardcoded")
					.Fluent(c => c.Indices.AliasExists(name))
					.Request(c => c.Indices.AliasExists(new AliasExistsRequest(name)))
					.FluentAsync(c => c.Indices.AliasExistsAsync(name))
					.RequestAsync(c => c.Indices.AliasExistsAsync(new AliasExistsRequest(name)))
				;

			await HEAD($"/index/_alias/hardcoded")
					.Fluent(c => c.Indices.AliasExists(name, b => b.Index(index)))
					.Request(c => c.Indices.AliasExists(new AliasExistsRequest(index, name)))
					.FluentAsync(c => c.Indices.AliasExistsAsync(name, b => b.Index(index)))
					.RequestAsync(c => c.Indices.AliasExistsAsync(new AliasExistsRequest(index, name)))
				;
		}
	}
}
