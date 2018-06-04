using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Role.GetRoleMapping
{
	public class GetRoleMappingUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/security/role_mapping")
				.Fluent(c => c.GetRoleMapping())
				.Request(c => c.GetRoleMapping(new GetRoleMappingRequest()))
				.FluentAsync(c => c.GetRoleMappingAsync())
				.RequestAsync(c => c.GetRoleMappingAsync(new GetRoleMappingRequest()))
				;

			var roles = "can_write,can_read_metadata";
			await GET($"/_xpack/security/role_mapping/{EscapeUriString(roles)}")
				.Fluent(c => c.GetRoleMapping(f=>f.Name(roles)))
				.Request(c => c.GetRoleMapping(new GetRoleMappingRequest(roles)))
				.FluentAsync(c => c.GetRoleMappingAsync(f=>f.Name(roles)))
				.RequestAsync(c => c.GetRoleMappingAsync(new GetRoleMappingRequest(roles)))
				;
		}
	}
}
