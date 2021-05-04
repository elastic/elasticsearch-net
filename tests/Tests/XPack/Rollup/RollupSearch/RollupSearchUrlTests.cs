// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Infer;

namespace Tests.XPack.Rollup.RollupSearch
{
	public class RollupSearchUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string index = "default-index";
			await POST($"/{index}/_rollup_search")
				.Fluent(c => c.Rollup.Search<Log>(s => s.Index(index)))
				.Request(c => c.Rollup.Search<Log>(new RollupSearchRequest(index)))
				.FluentAsync(c => c.Rollup.SearchAsync<Log>(s => s.Index(index)))
				.RequestAsync(c => c.Rollup.SearchAsync<Log>(new RollupSearchRequest(index)));

			await POST($"/_all/_rollup_search")
				.Fluent(c => c.Rollup.Search<Log>(s => s.Index(AllIndices)))
				.Request(c => c.Rollup.Search<Log>(new RollupSearchRequest(Nest.Indices.All)))
				.FluentAsync(c => c.Rollup.SearchAsync<Log>(s => s.Index(AllIndices)))
				.RequestAsync(c => c.Rollup.SearchAsync<Log>(new RollupSearchRequest(Nest.Indices.All)));
		}
	}
}
