using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.User.GetUser
{
	public class GetUserUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET("/_shield/user")
				.Fluent(c => c.GetUser())
				.Request(c => c.GetUser(new GetUserRequest()))
				.FluentAsync(c => c.GetUserAsync())
				.RequestAsync(c => c.GetUserAsync(new GetUserRequest()))
				;

			var users = "mpdreamz,gmarz,forloop";
			await UrlTester.GET($"/_shield/user/{UrlTester.EscapeUriString(users)}")
				.Fluent(c => c.GetUser(f=>f.Username(users)))
				.Request(c => c.GetUser(new GetUserRequest(users)))
				.FluentAsync(c => c.GetUserAsync(f=>f.Username(users)))
				.RequestAsync(c => c.GetUserAsync(new GetUserRequest(users)))
				;
		}
	}
}
