// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.UnfreezeIndex
{
	public class UnfreezeIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "project";
			await POST($"/{index}/_unfreeze")
					.Fluent(c => c.Indices.Unfreeze(index, s => s))
					.Request(c => c.Indices.Unfreeze(new UnfreezeIndexRequest(index)))
					.FluentAsync(c => c.Indices.UnfreezeAsync(index))
					.RequestAsync(c => c.Indices.UnfreezeAsync(new UnfreezeIndexRequest(index)))
				;
		}
	}
}
