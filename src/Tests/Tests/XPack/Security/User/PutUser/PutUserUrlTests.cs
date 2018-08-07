using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.User.PutUser
{
	public class PutUserUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.PUT("/_xpack/security/user/mpdreamz")
				.Fluent(c => c.PutUser("mpdreamz"))
				.Request(c => c.PutUser(new PutUserRequest("mpdreamz")))
				.FluentAsync(c => c.PutUserAsync("mpdreamz"))
				.RequestAsync(c => c.PutUserAsync(new PutUserRequest("mpdreamz")))
				;
		}
	}
}
