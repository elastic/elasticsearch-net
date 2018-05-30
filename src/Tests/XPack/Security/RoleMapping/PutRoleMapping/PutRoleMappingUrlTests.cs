using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.RoleMapping.PutRoleMapping
{
	public class PutRoleMappingUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			var role = "can_read";
			await PUT($"/_xpack/security/role_mapping/{role}")
				.Fluent(c => c.PutRoleMapping(role))
				.Request(c => c.PutRoleMapping(new PutRoleMappingRequest(role)))
				.FluentAsync(c => c.PutRoleMappingAsync(role))
				.RequestAsync(c => c.PutRoleMappingAsync(new PutRoleMappingRequest(role)))
				;
		}
	}
}
