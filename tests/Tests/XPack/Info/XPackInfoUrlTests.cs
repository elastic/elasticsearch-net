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
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Info
{
	public class XPackInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_xpack")
					.Fluent(c => c.XPack.Info())
					.Request(c => c.XPack.Info())
					.FluentAsync(c => c.XPack.InfoAsync())
					.RequestAsync(c => c.XPack.InfoAsync())
				;

			await GET("/_xpack/usage")
					.Fluent(c => c.XPack.Usage())
					.Request(c => c.XPack.Usage())
					.FluentAsync(c => c.XPack.UsageAsync())
					.RequestAsync(c => c.XPack.UsageAsync())
				;
		}
	}
}
