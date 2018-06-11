using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Role.DeleteRole
{
	public class DeleteRoleUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/security/role/mpdreamz")
				.Fluent(c => c.DeleteRole("mpdreamz"))
				.Request(c => c.DeleteRole(new DeleteRoleRequest("mpdreamz")))
				.FluentAsync(c => c.DeleteRoleAsync("mpdreamz"))
				.RequestAsync(c => c.DeleteRoleAsync(new DeleteRoleRequest("mpdreamz")))
				;
		}
	}
}
