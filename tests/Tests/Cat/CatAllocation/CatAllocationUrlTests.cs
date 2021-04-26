/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
