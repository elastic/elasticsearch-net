// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.DanglingIndices.Delete
{
	public class DeleteDanglingIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indexUuid = "indexuuid";

			await DELETE($"/_dangling/{indexUuid}")
					.Fluent(c => c.DanglingIndices.DeleteDanglingIndex(indexUuid))
					.Request(c => c.DanglingIndices.DeleteDanglingIndex(new DeleteDanglingIndexRequest(indexUuid)))
					.FluentAsync(c => c.DanglingIndices.DeleteDanglingIndexAsync(indexUuid))
					.RequestAsync(c => c.DanglingIndices.DeleteDanglingIndexAsync(new DeleteDanglingIndexRequest(indexUuid)))
				;
		}
	}
}
