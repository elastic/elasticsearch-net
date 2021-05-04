// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.AddBlock
{
	public class AddIndexBlockUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			var block = IndexBlock.ReadOnly;
			await PUT($"/{index}/_block/read_only")
					.Fluent(c => c.Indices.AddBlock(index, block))
					.Request(c => c.Indices.AddBlock(new AddIndexBlockRequest(index, block)))
					.FluentAsync(c => c.Indices.AddBlockAsync(index, block))
					.RequestAsync(c => c.Indices.AddBlockAsync(new AddIndexBlockRequest(index, block)))
				;
		}
	}
}
