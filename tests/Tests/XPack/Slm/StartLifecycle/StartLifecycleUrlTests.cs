using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.StartLifecycle
{
	public class StartLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_slm/start")
				.Fluent(c => c.SnapshotLifecycleManagement.Start())
				.Request(c => c.SnapshotLifecycleManagement.Start(new StartSnapshotLifecycleManagementRequest()))
				.FluentAsync(c => c.SnapshotLifecycleManagement.StartAsync())
				.RequestAsync(c => c.SnapshotLifecycleManagement.StartAsync(new StartSnapshotLifecycleManagementRequest()));
	}
}
