// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
