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
			await GET($"{index}/_xpack/rollup/data")
				.Fluent(c => c.GetRollupIndexCapabilities(index))
				.Request(c => c.GetRollupIndexCapabilities(new GetRollupIndexCapabilitiesRequest(index)))
				.FluentAsync(c => c.GetRollupIndexCapabilitiesAsync(index))
				.RequestAsync(c => c.GetRollupIndexCapabilitiesAsync(new GetRollupIndexCapabilitiesRequest(index)));
		}
	}
}
