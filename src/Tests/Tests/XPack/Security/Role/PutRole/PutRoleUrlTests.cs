using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.Role.PutRole
{
	public class PutRoleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_security/role/mpdreamz")
			.Fluent(c => c.Security.PutRole("mpdreamz", p => p))
			.Request(c => c.Security.PutRole(new PutRoleRequest("mpdreamz")))
			.FluentAsync(c => c.Security.PutRoleAsync("mpdreamz", p => p))
			.RequestAsync(c => c.Security.PutRoleAsync(new PutRoleRequest("mpdreamz")));
	}
}
