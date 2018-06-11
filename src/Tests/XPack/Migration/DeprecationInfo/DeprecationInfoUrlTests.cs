using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Migration.DeprecationInfo
{
	public class DeprecationInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/migration/deprecations")
				.Fluent(c => c.DeprecationInfo())
				.Request(c => c.DeprecationInfo(new DeprecationInfoRequest()))
				.FluentAsync(c => c.DeprecationInfoAsync())
				.RequestAsync(c => c.DeprecationInfoAsync(new DeprecationInfoRequest()))
				;

			var index = "another-index";

			await GET($"/{index}/_xpack/migration/deprecations")
				.Fluent(c => c.DeprecationInfo(d=>d.Index(index)))
				.Request(c => c.DeprecationInfo(new DeprecationInfoRequest(index)))
				.FluentAsync(c => c.DeprecationInfoAsync(d=>d.Index(index)))
				.RequestAsync(c => c.DeprecationInfoAsync(new DeprecationInfoRequest(index)))
				;
		}
	}
}
