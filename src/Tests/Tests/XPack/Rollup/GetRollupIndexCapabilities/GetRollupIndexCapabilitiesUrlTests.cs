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
				.Fluent(c => c.Rollup.GetIndexCapabilities(index))
				.Request(c => c.Rollup.GetIndexCapabilities(new GetRollupIndexCapabilitiesRequest(index)))
				.FluentAsync(c => c.Rollup.GetIndexCapabilitiesAsync(index))
				.RequestAsync(c => c.Rollup.GetIndexCapabilitiesAsync(new GetRollupIndexCapabilitiesRequest(index)));
		}
	}
}
