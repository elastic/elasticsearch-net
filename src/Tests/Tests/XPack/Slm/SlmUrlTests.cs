using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm
{
	public class SlmUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await PUT("/_slm/policy/policy_id")
					.Fluent(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycle("policy_id"))
					.Request(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycle(new PutSnapshotLifecycleRequest("policy_id")))
					.FluentAsync(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync("policy_id"))
					.RequestAsync(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync(new PutSnapshotLifecycleRequest("policy_id")))
				;

			await GET("/_slm/policy/policy_id")
					.Fluent(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(g => g.PolicyId("policy_id")))
					.Request(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(new GetSnapshotLifecycleRequest("policy_id")))
					.FluentAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(g => g.PolicyId("policy_id")))
					.RequestAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(new GetSnapshotLifecycleRequest("policy_id")))
				;

			await GET("/_slm/policy/policy_id1%2Cpolicy_id2")
					.Fluent(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(g => g.PolicyId("policy_id1,policy_id2")))
					.Request(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(new GetSnapshotLifecycleRequest(new [] { "policy_id1","policy_id2" })))
					.FluentAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(g => g.PolicyId("policy_id1,policy_id2")))
					.RequestAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(new GetSnapshotLifecycleRequest("policy_id1,policy_id2")))
				;

			await GET("/_slm/policy")
					.Fluent(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle())
					.Request(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(new GetSnapshotLifecycleRequest()))
					.FluentAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync())
					.RequestAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(new GetSnapshotLifecycleRequest()))
				;

			await PUT("/_slm/policy/policy_id/_execute")
					.Fluent(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle("policy_id"))
					.Request(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle(new ExecuteSnapshotLifecycleRequest("policy_id")))
					.FluentAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync("policy_id"))
					.RequestAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync(new ExecuteSnapshotLifecycleRequest("policy_id")))
				;

			await DELETE("/_slm/policy/policy_id")
					.Fluent(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle("policy_id"))
					.Request(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle(new DeleteSnapshotLifecycleRequest("policy_id")))
					.FluentAsync(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync("policy_id"))
					.RequestAsync(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync(new DeleteSnapshotLifecycleRequest("policy_id")))
				;
		}
	}
}
