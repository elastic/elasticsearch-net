using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Authenticate
{
	public class AuthenticateUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/security/_authenticate")
				.Fluent(c => c.Authenticate())
				.Request(c => c.Authenticate(new AuthenticateRequest()))
				.FluentAsync(c => c.AuthenticateAsync())
				.RequestAsync(c => c.AuthenticateAsync(new AuthenticateRequest()))
				;
		}
	}
}
