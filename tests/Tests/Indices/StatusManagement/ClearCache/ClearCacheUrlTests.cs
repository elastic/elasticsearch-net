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
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_cache/clear")
					.Fluent(c => c.Indices.ClearCache(All))
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest(All)))
					.FluentAsync(c => c.Indices.ClearCacheAsync(All))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest(All)))
				;
			await POST($"/_cache/clear")
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest()))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_cache/clear")
					.Fluent(c => c.Indices.ClearCache(index))
					.Request(c => c.Indices.ClearCache(new ClearCacheRequest(index)))
					.FluentAsync(c => c.Indices.ClearCacheAsync(index))
					.RequestAsync(c => c.Indices.ClearCacheAsync(new ClearCacheRequest(index)))
				;
		}
	}
}
