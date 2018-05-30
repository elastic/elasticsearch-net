using Nest;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.DisableUser
{
	public class DisableUserUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/security/user/ironman/_disable")
				.Fluent(c => c.DisableUser("ironman"))
				.Request(c => c.DisableUser(new DisableUserRequest("ironman")))
				.FluentAsync(c => c.DisableUserAsync("ironman"))
				.RequestAsync(c => c.DisableUserAsync(new DisableUserRequest("ironman")))
				;
		}
	}
}
