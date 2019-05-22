using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Info
{
	public class XPackInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_xpack")
					.Fluent(c => c.XPackInfo())
					.Request(c => c.XPackInfo())
					.FluentAsync(c => c.XPackInfoAsync())
					.RequestAsync(c => c.XPackInfoAsync())
				;

			await GET("/_xpack/usage")
					.Fluent(c => c.XPackUsage())
					.Request(c => c.XPackUsage())
					.FluentAsync(c => c.XPackUsageAsync())
					.RequestAsync(c => c.XPackUsageAsync())
				;
		}
	}
}
