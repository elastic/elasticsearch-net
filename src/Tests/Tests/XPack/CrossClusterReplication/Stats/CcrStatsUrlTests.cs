using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Stats
{
	public class CcrStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await UrlTester.GET($"/_ccr/stats")
				.Fluent(c => c.CrossClusterReplication.CcrStats(d => d))
				.Request(c => c.CrossClusterReplication.CcrStats(new CcrStatsRequest()))
				.FluentAsync(c => c.CrossClusterReplication.CcrStatsAsync(d => d))
				.RequestAsync(c => c.CrossClusterReplication.CcrStatsAsync(new CcrStatsRequest()));
	}
}
