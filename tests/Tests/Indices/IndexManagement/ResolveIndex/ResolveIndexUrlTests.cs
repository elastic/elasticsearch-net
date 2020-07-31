// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.ResolveIndex
{
	public class ResolveIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await GET($"/_resolve/index/{index}")
					.Fluent(c => c.Indices.Resolve(index))
					.Request(c => c.Indices.Resolve(new ResolveIndexRequest(index)))
					.FluentAsync(c => c.Indices.ResolveAsync(index))
					.RequestAsync(c => c.Indices.ResolveAsync(new ResolveIndexRequest(index)))
				;
		}
	}
}
