using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.GetRollupCapabilities
{
	public class GetRollupCapabilitiesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string index = "rollup-index";
			await GET($"_xpack/rollup/data/{index}")
				.Fluent(c => c.GetRollupCapabilities(j => j.Index(index)))
				.Request(c => c.GetRollupCapabilities(new GetRollupCapabilitiesRequest(index)))
				.FluentAsync(c => c.GetRollupCapabilitiesAsync(j => j.Index(index)))
				.RequestAsync(c => c.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest(index)));

			await GET($"_xpack/rollup/data/")
				.Fluent(c => c.GetRollupCapabilities())
				.Request(c => c.GetRollupCapabilities(new GetRollupCapabilitiesRequest()))
				.FluentAsync(c => c.GetRollupCapabilitiesAsync())
				.RequestAsync(c => c.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest()));
		}
	}
}
