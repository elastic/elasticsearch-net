using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.User.PutUser
{
	public class PutUserUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.PUT("/_security/user/mpdreamz")
			.Fluent(c => c.Security.PutUser("mpdreamz"))
			.Request(c => c.Security.PutUser(new PutUserRequest("mpdreamz")))
			.FluentAsync(c => c.Security.PutUserAsync("mpdreamz"))
			.RequestAsync(c => c.Security.PutUserAsync(new PutUserRequest("mpdreamz")));
	}
}
