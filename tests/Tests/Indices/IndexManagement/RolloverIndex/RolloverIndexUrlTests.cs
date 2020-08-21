// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.RolloverIndex
{
	public class RolloverIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var alias = "alias1";
			await POST($"/{alias}/_rollover")
				.Fluent(c => c.Indices.Rollover(alias))
				.Request(c => c.Indices.Rollover(new RolloverIndexRequest(alias)))
				.FluentAsync(c => c.Indices.RolloverAsync(alias))
				.RequestAsync(c => c.Indices.RolloverAsync(new RolloverIndexRequest(alias)));

			var index = "newindex";

			await POST($"/{alias}/_rollover/{index}")
				.Fluent(c => c.Indices.Rollover(alias, r => r.NewIndex(index)))
				.Request(c => c.Indices.Rollover(new RolloverIndexRequest(alias, index)))
				.FluentAsync(c => c.Indices.RolloverAsync(alias, r => r.NewIndex(index)))
				.RequestAsync(c => c.Indices.RolloverAsync(new RolloverIndexRequest(alias, index)));
		}
	}
}
