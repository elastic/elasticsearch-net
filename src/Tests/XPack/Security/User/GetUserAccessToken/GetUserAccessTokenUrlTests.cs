using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.GetUserAccessToken
{
	public class GetUserAccessTokenUserUrlTests : IUrlTests
	{
		[U]
		public async Task Urls()
		{
			var u = ClusterAuthentication.Admin.Username;
			var p = ClusterAuthentication.Admin.Password;
			await POST("/_xpack/security/oauth2/token")
				.Fluent(c => c.GetUserAccessToken(u,p))
				.Request(c => c.GetUserAccessToken(new GetUserAccessTokenRequest(u,p)))
				.FluentAsync(c => c.GetUserAccessTokenAsync(u, p))
				.RequestAsync(c => c.GetUserAccessTokenAsync(new GetUserAccessTokenRequest(u, p)));
		}
	}
}
