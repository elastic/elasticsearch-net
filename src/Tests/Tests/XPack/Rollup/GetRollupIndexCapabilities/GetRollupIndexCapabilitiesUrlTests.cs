using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.GetRollupIndexCapabilities
{
	public class GetRollupIndexCapabilitiesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string index = "rollup-index";
			await GET($"{index}/_rollup/data")
				.Fluent(c => c.Rollup.GetRollupIndexCapabilities(index))
				.Request(c => c.Rollup.GetRollupIndexCapabilities(new GetRollupIndexCapabilitiesRequest(index)))
				.FluentAsync(c => c.Rollup.GetRollupIndexCapabilitiesAsync(index))
				.RequestAsync(c => c.Rollup.GetRollupIndexCapabilitiesAsync(new GetRollupIndexCapabilitiesRequest(index)));
		}
	}
}
