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

namespace Tests.Search.PointInTime
{
	public class OpenPointInTimeUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_pit")
				.Fluent(c => c.OpenPointInTime())
				.Request(c => c.OpenPointInTime(new OpenPointInTimeRequest()))
				.FluentAsync(c => c.OpenPointInTimeAsync())
				.RequestAsync(c => c.OpenPointInTimeAsync(new OpenPointInTimeRequest()));

			const string index = "devs";

			await POST($"/{index}/_pit")
				.Fluent(c => c.OpenPointInTime(index))
				.Request(c => c.OpenPointInTime(new OpenPointInTimeRequest(index)))
				.FluentAsync(c => c.OpenPointInTimeAsync(index))
				.RequestAsync(c => c.OpenPointInTimeAsync(new OpenPointInTimeRequest(index)));
		}
	}

	public class ClosePointInTimeUrlTests
	{
		[U] public async Task Urls() =>
			await DELETE("/_pit")
				.Fluent(c => c.ClosePointInTime())
				.Request(c => c.ClosePointInTime(new ClosePointInTimeRequest()))
				.FluentAsync(c => c.ClosePointInTimeAsync())
				.RequestAsync(c => c.ClosePointInTimeAsync(new ClosePointInTimeRequest()));
	}
}
