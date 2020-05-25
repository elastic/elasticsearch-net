using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.StartLifecycle
{
	public class GetLifecycleStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_slm/stats")
				.Fluent(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats())
				.Request(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats(new GetSnapshotLifecycleStatsRequest()))
				.FluentAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync())
				.RequestAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync(new GetSnapshotLifecycleStatsRequest()));
	}
}
