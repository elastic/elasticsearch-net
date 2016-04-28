using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	public class ClearCachedRolesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			var role = "some_role";
			await UrlTester.POST($"/_shield/role/{role}/_clear_cache")
				.Fluent(c => c.ClearCachedRoles(role))
				.Request(c => c.ClearCachedRoles(new ClearCachedRolesRequest(role)))
				.FluentAsync(c => c.ClearCachedRolesAsync(role))
				.RequestAsync(c => c.ClearCachedRolesAsync(new ClearCachedRolesRequest(role)))
				;
		}
	}
}
