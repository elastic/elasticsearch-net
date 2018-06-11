using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	public class InvalidateUserAccessTokenUserUrlTests : IUrlTests
	{
		[U]
		public async Task Urls()
		{
			var token = "some_token";
			await DELETE("/_xpack/security/oauth2/token")
				.Fluent(c => c.InvalidateUserAccessToken(token))
				.Request(c => c.InvalidateUserAccessToken(new InvalidateUserAccessTokenRequest(token)))
				.FluentAsync(c => c.InvalidateUserAccessTokenAsync(token))
				.RequestAsync(c => c.InvalidateUserAccessTokenAsync(new InvalidateUserAccessTokenRequest(token)));
		}
	}
}
