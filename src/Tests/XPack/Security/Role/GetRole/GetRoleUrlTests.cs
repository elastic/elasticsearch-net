using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Role.GetRole
{
	public class GetRoleUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_shield/role")
				.Fluent(c => c.GetRole())
				.Request(c => c.GetRole(new GetRoleRequest()))
				.FluentAsync(c => c.GetRoleAsync())
				.RequestAsync(c => c.GetRoleAsync(new GetRoleRequest()))
				;

			var users = "admin,user,nest_user";
			await GET($"/_shield/role/{EscapeUriString(users)}")
				.Fluent(c => c.GetRole(f=>f.Name(users)))
				.Request(c => c.GetRole(new GetRoleRequest(users)))
				.FluentAsync(c => c.GetRoleAsync(f=>f.Name(users)))
				.RequestAsync(c => c.GetRoleAsync(new GetRoleRequest(users)))
				;
		}
	}
}
