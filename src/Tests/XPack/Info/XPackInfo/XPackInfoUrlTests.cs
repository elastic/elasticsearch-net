using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Info.XPackInfo
{
	public class XPackInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET("/_xpack")
				.Fluent(c => c.XPackInfo())
				.Request(c => c.XPackInfo(new XPackInfoRequest()))
				.FluentAsync(c => c.XPackInfoAsync())
				.RequestAsync(c => c.XPackInfoAsync(new XPackInfoRequest()))
				;
		}
	}
}
