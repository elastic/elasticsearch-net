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
			const string id = "rollup-id";
			await GET($"_rollup/data/{id}")
				.Fluent(c => c.Rollup.GetRollupCapabilities(j => j.Id(id)))
				.Request(c => c.Rollup.GetRollupCapabilities(new GetRollupCapabilitiesRequest(id)))
				.FluentAsync(c => c.Rollup.GetRollupCapabilitiesAsync(j => j.Id(id)))
				.RequestAsync(c => c.Rollup.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest(id)));

			await GET($"_rollup/data/")
				.Fluent(c => c.Rollup.GetRollupCapabilities())
				.Request(c => c.Rollup.GetRollupCapabilities(new GetRollupCapabilitiesRequest()))
				.FluentAsync(c => c.Rollup.GetRollupCapabilitiesAsync())
				.RequestAsync(c => c.Rollup.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest()));
		}
	}
}
