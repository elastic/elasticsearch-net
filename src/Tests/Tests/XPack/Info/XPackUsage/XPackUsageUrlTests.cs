using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Info.XPackUsage
{
	public class XPackUsageUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.GET("/_xpack/usage")
				.Fluent(c => c.XPackUsage())
				.Request(c => c.XPackUsage(new XPackUsageRequest()))
				.FluentAsync(c => c.XPackUsageAsync())
				.RequestAsync(c => c.XPackUsageAsync(new XPackUsageRequest()))
				;
		}
	}
}
