using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.User.GetUser
{
	public class GetUserUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_security/user")
					.Fluent(c => c.Security.GetUser())
					.Request(c => c.Security.GetUser(new GetUserRequest()))
					.FluentAsync(c => c.Security.GetUserAsync())
					.RequestAsync(c => c.Security.GetUserAsync(new GetUserRequest()))
				;

			var users = "mpdreamz,gmarz,forloop";
			await GET($"/_security/user/{EscapeUriString(users)}")
					.Fluent(c => c.Security.GetUser(f => f.Username(users)))
					.Request(c => c.Security.GetUser(new GetUserRequest(users)))
					.FluentAsync(c => c.Security.GetUserAsync(f => f.Username(users)))
					.RequestAsync(c => c.Security.GetUserAsync(new GetUserRequest(users)))
				;
		}
	}
}
