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
