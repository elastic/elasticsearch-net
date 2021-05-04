// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatAllocation
{
	public class CatAllocationUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/allocation")
					.Fluent(c => c.Cat.Allocation())
					.Request(c => c.Cat.Allocation(new CatAllocationRequest()))
					.FluentAsync(c => c.Cat.AllocationAsync())
					.RequestAsync(c => c.Cat.AllocationAsync(new CatAllocationRequest()))
				;

			await GET("/_cat/allocation/foo")
					.Fluent(c => c.Cat.Allocation(a => a.NodeId("foo")))
					.Request(c => c.Cat.Allocation(new CatAllocationRequest("foo")))
					.FluentAsync(c => c.Cat.AllocationAsync(a => a.NodeId("foo")))
					.RequestAsync(c => c.Cat.AllocationAsync(new CatAllocationRequest("foo")))
				;
		}
	}
}
