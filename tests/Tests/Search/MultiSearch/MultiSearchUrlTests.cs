// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.MultiSearch
{
	public class MultiSearchUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "indexx";
			await POST($"/_msearch")
					.Fluent(c => c.MultiSearch())
					.Request(c => c.MultiSearch(new MultiSearchRequest()))
					.FluentAsync(c => c.MultiSearchAsync())
					.RequestAsync(c => c.MultiSearchAsync(new MultiSearchRequest()))
				;

			await POST($"/{index}/_msearch")
					.Fluent(c => c.MultiSearch(index))
					.Request(c => c.MultiSearch(new MultiSearchRequest(index)))
					.FluentAsync(c => c.MultiSearchAsync(index))
					.RequestAsync(c => c.MultiSearchAsync(new MultiSearchRequest(index)))
				;
		}
	}
}
