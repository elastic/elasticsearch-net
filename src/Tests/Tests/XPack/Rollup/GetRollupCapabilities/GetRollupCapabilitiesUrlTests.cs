using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.GetRollupCapabilities
{
	public class GetRollupCapabilitiesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await GET($"_rollup/data/{id}")
				.Fluent(c => c.Rollup.GetCapabilities(j => j.Id(id)))
				.Request(c => c.Rollup.GetCapabilities(new GetRollupCapabilitiesRequest(id)))
				.FluentAsync(c => c.Rollup.GetCapabilitiesAsync(j => j.Id(id)))
				.RequestAsync(c => c.Rollup.GetCapabilitiesAsync(new GetRollupCapabilitiesRequest(id)));

			await GET($"_rollup/data/")
				.Fluent(c => c.Rollup.GetCapabilities())
				.Request(c => c.Rollup.GetCapabilities(new GetRollupCapabilitiesRequest()))
				.FluentAsync(c => c.Rollup.GetCapabilitiesAsync())
				.RequestAsync(c => c.Rollup.GetCapabilitiesAsync(new GetRollupCapabilitiesRequest()));
		}
	}
}
