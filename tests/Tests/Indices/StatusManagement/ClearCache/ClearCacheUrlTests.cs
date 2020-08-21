// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_cache/clear")
					.Fluent(c => c.Indices.ClearCache(All))
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest(All)))
					.FluentAsync(c => c.Indices.ClearCacheAsync(All))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest(All)))
				;
			await POST($"/_cache/clear")
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest()))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_cache/clear")
					.Fluent(c => c.Indices.ClearCache(index))
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest(index)))
					.FluentAsync(c => c.Indices.ClearCacheAsync(index))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest(index)))
				;
		}
	}
}
