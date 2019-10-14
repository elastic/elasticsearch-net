using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.ExecuteLifecycle
{
	public class ExecuteLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await PUT("/_slm/policy/policy_id/_execute")
				.Fluent(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle("policy_id"))
				.Request(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle(new ExecuteSnapshotLifecycleRequest("policy_id")))
				.FluentAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync("policy_id"))
				.RequestAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync(new ExecuteSnapshotLifecycleRequest("policy_id")));
	}
}
