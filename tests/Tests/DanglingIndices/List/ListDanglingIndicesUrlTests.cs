// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.DanglingIndices.List
{
	public class ListDanglingIndicesUrlTests
	{
		[U] public async Task Urls() =>
			await GET($"/_dangling")
				.Fluent(c => c.DanglingIndices.List())
				.Request(c => c.DanglingIndices.List(new ListDanglingIndicesRequest()))
				.FluentAsync(c => c.DanglingIndices.ListAsync())
				.RequestAsync(c => c.DanglingIndices.ListAsync(new ListDanglingIndicesRequest()));
	}
}
