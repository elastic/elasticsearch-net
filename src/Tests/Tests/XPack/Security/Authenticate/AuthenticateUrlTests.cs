using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.Authenticate
{
	public class AuthenticateUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_security/_authenticate")
			.Fluent(c => c.Security.Authenticate())
			.Request(c => c.Security.Authenticate(new AuthenticateRequest()))
			.FluentAsync(c => c.Security.AuthenticateAsync())
			.RequestAsync(c => c.Security.AuthenticateAsync(new AuthenticateRequest()));
	}
}
