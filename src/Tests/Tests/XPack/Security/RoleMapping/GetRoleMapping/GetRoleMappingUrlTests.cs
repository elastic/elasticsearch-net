using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Role.GetRoleMapping
{
	public class GetRoleMappingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_security/role_mapping")
					.Fluent(c => c.Security.GetRoleMapping())
					.Request(c => c.Security.GetRoleMapping(new GetRoleMappingRequest()))
					.FluentAsync(c => c.Security.GetRoleMappingAsync())
					.RequestAsync(c => c.Security.GetRoleMappingAsync(new GetRoleMappingRequest()))
				;

			var roles = "can_write,can_read_metadata";
			await GET($"/_security/role_mapping/{EscapeUriString(roles)}")
					.Fluent(c => c.Security.GetRoleMapping(f => f.Name(roles)))
					.Request(c => c.Security.GetRoleMapping(new GetRoleMappingRequest(roles)))
					.FluentAsync(c => c.Security.GetRoleMappingAsync(f => f.Name(roles)))
					.RequestAsync(c => c.Security.GetRoleMappingAsync(new GetRoleMappingRequest(roles)))
				;
		}
	}
}
