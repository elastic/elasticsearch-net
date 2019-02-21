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
				.Fluent(c => c.GetRollupCapabilities(j => j.Id(id)))
				.Request(c => c.GetRollupCapabilities(new GetRollupCapabilitiesRequest(id)))
				.FluentAsync(c => c.GetRollupCapabilitiesAsync(j => j.Id(id)))
				.RequestAsync(c => c.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest(id)));

			await GET($"_rollup/data/")
				.Fluent(c => c.GetRollupCapabilities())
				.Request(c => c.GetRollupCapabilities(new GetRollupCapabilitiesRequest()))
				.FluentAsync(c => c.GetRollupCapabilitiesAsync())
				.RequestAsync(c => c.GetRollupCapabilitiesAsync(new GetRollupCapabilitiesRequest()));
		}
	}
}
