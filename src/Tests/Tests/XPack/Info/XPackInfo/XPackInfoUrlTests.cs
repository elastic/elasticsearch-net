using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Info.XPackInfo
{
	public class XPackInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_xpack")
			.Fluent(c => c.XPackInfo())
			.Request(c => c.XPackInfo(new XPackInfoRequest()))
			.FluentAsync(c => c.XPackInfoAsync())
			.RequestAsync(c => c.XPackInfoAsync(new XPackInfoRequest()));
	}
}
