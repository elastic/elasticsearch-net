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

namespace Tests.XPack.Security.User.ChangePassword
{
	public class DeleteUserUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls()
		{
			await PUT("/_security/user/forloop/_password")
					.Fluent(c => c.Security.ChangePassword(p => p.Username("forloop").Password("password")))
					.Request(c => c.Security.ChangePassword(new ChangePasswordRequest("forloop") { Password = "password" }))
					.FluentAsync(c => c.Security.ChangePasswordAsync(p => p.Username("forloop").Password("password")))
					.RequestAsync(c => c.Security.ChangePasswordAsync(new ChangePasswordRequest("forloop") { Password = "password" }))
				;

			await PUT("/_security/user/_password")
					.Fluent(c => c.Security.ChangePassword(p => p.Password("password")))
					.Request(c => c.Security.ChangePassword(new ChangePasswordRequest() { Password = "password" }))
					.FluentAsync(c => c.Security.ChangePasswordAsync(p => p.Password("password")))
					.RequestAsync(c => c.Security.ChangePasswordAsync(new ChangePasswordRequest() { Password = "password" }))
				;
		}
	}
}
