using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	public class InvalidateUserAccessTokenUserUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls()
		{
			var token = "some_token";
			await DELETE("/_security/oauth2/token")
				.Fluent(c => c.Security.InvalidateUserAccessToken(token))
				.Request(c => c.Security.InvalidateUserAccessToken(new InvalidateUserAccessTokenRequest(token)))
				.FluentAsync(c => c.Security.InvalidateUserAccessTokenAsync(token))
				.RequestAsync(c => c.Security.InvalidateUserAccessTokenAsync(new InvalidateUserAccessTokenRequest(token)));
		}
	}
}
