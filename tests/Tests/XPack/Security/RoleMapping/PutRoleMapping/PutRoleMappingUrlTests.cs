// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.RoleMapping.PutRoleMapping
{
	public class PutRoleMappingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var role = "can_read";
			await PUT($"/_security/role_mapping/{role}")
					.Fluent(c => c.Security.PutRoleMapping(role, p => p))
					.Request(c => c.Security.PutRoleMapping(new PutRoleMappingRequest(role)))
					.FluentAsync(c => c.Security.PutRoleMappingAsync(role, p => p))
					.RequestAsync(c => c.Security.PutRoleMappingAsync(new PutRoleMappingRequest(role)))
				;
		}
	}
}
