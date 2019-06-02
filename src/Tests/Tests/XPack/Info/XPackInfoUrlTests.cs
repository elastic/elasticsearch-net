using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Info
{
	public class XPackInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_xpack")
					.Fluent(c => c.XPack.Info())
					.Request(c => c.XPack.Info())
					.FluentAsync(c => c.XPack.InfoAsync())
					.RequestAsync(c => c.XPack.InfoAsync())
				;

			await GET("/_xpack/usage")
					.Fluent(c => c.XPack.Usage())
					.Request(c => c.XPack.Usage())
					.FluentAsync(c => c.XPack.UsageAsync())
					.RequestAsync(c => c.XPack.UsageAsync())
				;
		}
	}
}
