using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.RoleMapping.DeleteRoleMapping
{
	public class DeleteRoleMappingUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			var role = "can_read";
			await DELETE($"/_xpack/security/role_mapping/{role}")
				.Fluent(c => c.DeleteRoleMapping(role))
				.Request(c => c.DeleteRoleMapping(new DeleteRoleMappingRequest(role)))
				.FluentAsync(c => c.DeleteRoleMappingAsync(role))
				.RequestAsync(c => c.DeleteRoleMappingAsync(new DeleteRoleMappingRequest(role)))
				;
		}
	}
}
