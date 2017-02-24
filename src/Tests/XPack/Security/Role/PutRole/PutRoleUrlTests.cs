using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Role.PutRole
{
	public class PutRoleUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/security/role/mpdreamz")
				.Fluent(c => c.PutRole("mpdreamz"))
				.Request(c => c.PutRole(new PutRoleRequest("mpdreamz")))
				.FluentAsync(c => c.PutRoleAsync("mpdreamz"))
				.RequestAsync(c => c.PutRoleAsync(new PutRoleRequest("mpdreamz")))
				;
		}
	}
}
