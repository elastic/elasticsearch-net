// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.RoleMapping.DeleteRoleMapping
{
	public class DeleteRoleMappingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var role = "can_read";
			await DELETE($"/_security/role_mapping/{role}")
					.Fluent(c => c.Security.DeleteRoleMapping(role))
					.Request(c => c.Security.DeleteRoleMapping(new DeleteRoleMappingRequest(role)))
					.FluentAsync(c => c.Security.DeleteRoleMappingAsync(role))
					.RequestAsync(c => c.Security.DeleteRoleMappingAsync(new DeleteRoleMappingRequest(role)))
				;
		}
	}
}
