using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

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
