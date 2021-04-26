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

namespace Tests.XPack.Security.ClearCachedRealms
{
	public class ClearCachedRealmsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_security/realm/mpdreamz/_clear_cache")
					.Fluent(c => c.Security.ClearCachedRealms("mpdreamz"))
					.Request(c => c.Security.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz")))
					.FluentAsync(c => c.Security.ClearCachedRealmsAsync("mpdreamz"))
					.RequestAsync(c => c.Security.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz")))
				;


			var users = "mpdreamz,gmarz,forloop";
			await POST($"/_security/realm/mpdreamz/_clear_cache?usernames={EscapeUriString(users)}")
					.Fluent(c => c.Security.ClearCachedRealms("mpdreamz", f => f.Usernames(users)))
					.Request(c => c.Security.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
					.FluentAsync(c => c.Security.ClearCachedRealmsAsync("mpdreamz", f => f.Usernames(users)))
					.RequestAsync(c => c.Security.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
				;
		}
	}
}
