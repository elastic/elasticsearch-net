// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.DanglingIndices.Import
{
	public class ImportDanglingIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indexUuid = "indexuuid";

			await POST($"/_dangling/{indexUuid}")
					.Fluent(c => c.DanglingIndices.ImportDanglingIndex(indexUuid))
					.Request(c => c.DanglingIndices.ImportDanglingIndex(new ImportDanglingIndexRequest(indexUuid)))
					.FluentAsync(c => c.DanglingIndices.ImportDanglingIndexAsync(indexUuid))
					.RequestAsync(c => c.DanglingIndices.ImportDanglingIndexAsync(new ImportDanglingIndexRequest(indexUuid)))
				;
		}
	}
}
