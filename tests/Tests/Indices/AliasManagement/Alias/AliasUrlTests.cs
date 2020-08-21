// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasUrlTests
	{
		[U] public async Task Urls() => await POST($"/_aliases")
			.Fluent(c => c.Indices.BulkAlias(b => b))
			.Request(c => c.Indices.BulkAlias(new BulkAliasRequest()))
			.FluentAsync(c => c.Indices.BulkAliasAsync(b => b))
			.RequestAsync(c => c.Indices.BulkAliasAsync(new BulkAliasRequest()));
	}
}
