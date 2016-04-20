using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Shield.User.PutUser
{
	public class PutUserUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_shield/user/mpdreamz")
				.Fluent(c => c.PutUser("mpdreamz"))
				.Request(c => c.PutUser(new PutUserRequest("mpdreamz")))
				.FluentAsync(c => c.PutUserAsync("mpdreamz"))
				.RequestAsync(c => c.PutUserAsync(new PutUserRequest("mpdreamz")))
				;
		}
	}
}
