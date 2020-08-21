// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	public class ClearCachedRolesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var role = "some_role";
			await POST($"/_security/role/{role}/_clear_cache")
					.Fluent(c => c.Security.ClearCachedRoles(role))
					.Request(c => c.Security.ClearCachedRoles(new ClearCachedRolesRequest(role)))
					.FluentAsync(c => c.Security.ClearCachedRolesAsync(role))
					.RequestAsync(c => c.Security.ClearCachedRolesAsync(new ClearCachedRolesRequest(role)))
				;
		}
	}
}
