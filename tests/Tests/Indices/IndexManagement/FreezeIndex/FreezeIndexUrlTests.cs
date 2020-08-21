// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.FreezeIndex
{
	public class FreezeIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "project";
			await POST($"/{index}/_freeze")
					.Fluent(c => c.Indices.Freeze(index, s => s))
					.Request(c => c.Indices.Freeze(new FreezeIndexRequest(index)))
					.FluentAsync(c => c.Indices.FreezeAsync(index))
					.RequestAsync(c => c.Indices.FreezeAsync(new FreezeIndexRequest(index)))
				;
		}
	}
}
