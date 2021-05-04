// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.PutAlias
{
	public class PutAliasUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await PUT($"/{index}/_alias/{hardcoded}")
					.Fluent(c => c.Indices.PutAlias(index, hardcoded))
					.Request(c => c.Indices.PutAlias(new PutAliasRequest(index, hardcoded)))
					.FluentAsync(c => c.Indices.PutAliasAsync(index, hardcoded))
					.RequestAsync(c => c.Indices.PutAliasAsync(new PutAliasRequest(index, hardcoded)))
				;
		}
	}
}
