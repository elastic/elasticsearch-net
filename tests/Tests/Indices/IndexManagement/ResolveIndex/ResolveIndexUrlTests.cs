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

namespace Tests.Indices.IndexManagement.ResolveIndex
{
	public class ResolveIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await GET($"/_resolve/index/{index}")
					.Fluent(c => c.Indices.Resolve(index))
					.Request(c => c.Indices.Resolve(new ResolveIndexRequest(index)))
					.FluentAsync(c => c.Indices.ResolveAsync(index))
					.RequestAsync(c => c.Indices.ResolveAsync(new ResolveIndexRequest(index)))
				;
		}
	}
}
