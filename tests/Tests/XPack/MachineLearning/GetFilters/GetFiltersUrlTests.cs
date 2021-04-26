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

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/filters")
				.Fluent(c => c.MachineLearning.GetFilters(p => p))
				.Request(c => c.MachineLearning.GetFilters(new GetFiltersRequest()))
				.FluentAsync(c => c.MachineLearning.GetFiltersAsync(p => p))
				.RequestAsync(c => c.MachineLearning.GetFiltersAsync(new GetFiltersRequest()));

			await GET("/_ml/filters/1")
				.Request(c => c.MachineLearning.GetFilters(new GetFiltersRequest(1)))
				.Fluent(c => c.MachineLearning.GetFilters(r => r.FilterId(1)))
				.FluentAsync(c => c.MachineLearning.GetFiltersAsync(r => r.FilterId(1)))
				.RequestAsync(c => c.MachineLearning.GetFiltersAsync(new GetFiltersRequest(1)));
		}
	}
}
