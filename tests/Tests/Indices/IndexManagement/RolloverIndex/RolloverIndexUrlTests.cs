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
