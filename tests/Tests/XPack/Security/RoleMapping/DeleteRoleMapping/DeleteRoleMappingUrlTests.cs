using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
