// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Security.RoleMapping.GetRoleMapping
{
	public class GetRoleMappingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.GET("/_security/role_mapping")
					.Fluent(c => c.Security.GetRoleMapping())
					.Request(c => c.Security.GetRoleMapping(new GetRoleMappingRequest()))
					.FluentAsync(c => c.Security.GetRoleMappingAsync())
					.RequestAsync(c => c.Security.GetRoleMappingAsync(new GetRoleMappingRequest()))
				;

			var roles = "can_write,can_read_metadata";
			await UrlTester.GET($"/_security/role_mapping/{UrlTester.EscapeUriString(roles)}")
					.Fluent(c => c.Security.GetRoleMapping(roles))
					.Request(c => c.Security.GetRoleMapping(new GetRoleMappingRequest(roles)))
					.FluentAsync(c => c.Security.GetRoleMappingAsync(roles))
					.RequestAsync(c => c.Security.GetRoleMappingAsync(new GetRoleMappingRequest(roles)))
				;
		}
	}
}
