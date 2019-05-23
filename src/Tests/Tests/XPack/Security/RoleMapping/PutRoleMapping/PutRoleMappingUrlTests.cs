using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.RoleMapping.PutRoleMapping
{
	public class PutRoleMappingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var role = "can_read";
			await PUT($"/_security/role_mapping/{role}")
					.Fluent(c => c.Security.PutRoleMapping(role))
					.Request(c => c.Security.PutRoleMapping(new PutRoleMappingRequest(role)))
					.FluentAsync(c => c.Security.PutRoleMappingAsync(role))
					.RequestAsync(c => c.Security.PutRoleMappingAsync(new PutRoleMappingRequest(role)))
				;
		}
	}
}
