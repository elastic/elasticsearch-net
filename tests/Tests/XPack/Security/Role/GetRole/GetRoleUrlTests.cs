// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.Role.GetRole
{
	public class GetRoleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_security/role")
					.Fluent(c => c.Security.GetRole())
					.Request(c => c.Security.GetRole(new GetRoleRequest()))
					.FluentAsync(c => c.Security.GetRoleAsync())
					.RequestAsync(c => c.Security.GetRoleAsync(new GetRoleRequest()))
				;

			var users = "admin,user,nest_user";
			await GET($"/_security/role/{EscapeUriString(users)}")
					.Fluent(c => c.Security.GetRole(users))
					.Request(c => c.Security.GetRole(new GetRoleRequest(users)))
					.FluentAsync(c => c.Security.GetRoleAsync(users))
					.RequestAsync(c => c.Security.GetRoleAsync(new GetRoleRequest(users)))
				;
		}
	}
}
