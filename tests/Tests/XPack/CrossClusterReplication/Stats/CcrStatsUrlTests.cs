using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Stats
{
	public class CcrStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await UrlTester.GET($"/_ccr/stats")
				.Fluent(c => c.CrossClusterReplication.Stats(d => d))
				.Request(c => c.CrossClusterReplication.Stats(new CcrStatsRequest()))
				.FluentAsync(c => c.CrossClusterReplication.StatsAsync(d => d))
				.RequestAsync(c => c.CrossClusterReplication.StatsAsync(new CcrStatsRequest()));
	}
}
