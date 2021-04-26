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

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_refresh")
					.Request(c => c.Indices.Refresh(new RefreshRequest()))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest()))
				;

			await POST($"/_all/_refresh")
					.Fluent(c => c.Indices.Refresh(All))
					.Request(c => c.Indices.Refresh(new RefreshRequest(All)))
					.FluentAsync(c => c.Indices.RefreshAsync(All))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest(All)))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_refresh")
					.Fluent(c => c.Indices.Refresh(index))
					.Request(c => c.Indices.Refresh(new RefreshRequest(index)))
					.FluentAsync(c => c.Indices.RefreshAsync(index))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest(index)))
				;
		}
	}
}
