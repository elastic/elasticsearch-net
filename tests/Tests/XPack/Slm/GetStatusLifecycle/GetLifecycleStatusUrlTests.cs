using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.GetStatusLifecycle
{
	public class GetLifecycleStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_slm/status")
				.Fluent(c => c.SnapshotLifecycleManagement.GetStatus())
				.Request(c => c.SnapshotLifecycleManagement.GetStatus(new GetSnapshotLifecycleManagementStatusRequest()))
				.FluentAsync(c => c.SnapshotLifecycleManagement.GetStatusAsync())
				.RequestAsync(c => c.SnapshotLifecycleManagement.GetStatusAsync(new GetSnapshotLifecycleManagementStatusRequest()));
	}
}
