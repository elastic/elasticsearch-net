using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.GetUser
{
	public class GetUserUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/security/user")
				.Fluent(c => c.GetUser())
				.Request(c => c.GetUser(new GetUserRequest()))
				.FluentAsync(c => c.GetUserAsync())
				.RequestAsync(c => c.GetUserAsync(new GetUserRequest()))
				;

			var users = "mpdreamz,gmarz,forloop";
			await GET($"/_xpack/security/user/{EscapeUriString(users)}")
				.Fluent(c => c.GetUser(f=>f.Username(users)))
				.Request(c => c.GetUser(new GetUserRequest(users)))
				.FluentAsync(c => c.GetUserAsync(f=>f.Username(users)))
				.RequestAsync(c => c.GetUserAsync(new GetUserRequest(users)))
				;
		}
	}
}
