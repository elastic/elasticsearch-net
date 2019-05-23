using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Ilm
{
	public class IlmUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await DELETE("/_ilm/policy/policy_id")
					.Fluent(c => c.IndexLifecycleManagement.DeleteLifecycle("policy_id"))
					.Request(c => c.IndexLifecycleManagement.DeleteLifecycle(new DeleteLifecycleRequest("policy_id")))
					.FluentAsync(c => c.IndexLifecycleManagement.DeleteLifecycleAsync("policy_id"))
					.RequestAsync(c => c.IndexLifecycleManagement.DeleteLifecycleAsync(new DeleteLifecycleRequest("policy_id")))
				;

			await GET("/index/_ilm/explain")
					.Fluent(c => c.IndexLifecycleManagement.ExplainLifecycle("index"))
					.Request(c => c.IndexLifecycleManagement.ExplainLifecycle(new ExplainLifecycleRequest("index")))
					.FluentAsync(c => c.IndexLifecycleManagement.ExplainLifecycleAsync("index"))
					.RequestAsync(c => c.IndexLifecycleManagement.ExplainLifecycleAsync(new ExplainLifecycleRequest("index")))
				;

			await GET("/_ilm/policy")
					.Fluent(c => c.IndexLifecycleManagement.GetLifecycle())
					.Request(c => c.IndexLifecycleManagement.GetLifecycle(new GetLifecycleRequest()))
					.FluentAsync(c => c.IndexLifecycleManagement.GetLifecycleAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.GetLifecycleAsync(new GetLifecycleRequest()))
				;

			await GET("/_ilm/status")
					.Fluent(c => c.IndexLifecycleManagement.GetIlmStatus())
					.Request(c => c.IndexLifecycleManagement.GetIlmStatus())
					.FluentAsync(c => c.IndexLifecycleManagement.GetIlmStatusAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.GetIlmStatusAsync())
				;

			await POST("/_ilm/move/index")
					.Fluent(c => c.IndexLifecycleManagement.MoveToStep("index"))
					.Request(c => c.IndexLifecycleManagement.MoveToStep(new MoveToStepRequest("index")))
					.FluentAsync(c => c.IndexLifecycleManagement.MoveToStepAsync("index"))
					.RequestAsync(c => c.IndexLifecycleManagement.MoveToStepAsync(new MoveToStepRequest("index")))
				;

			await PUT("/_ilm/policy/policy_id")
					.Fluent(c => c.IndexLifecycleManagement.PutLifecycle("policy_id"))
					.Request(c => c.IndexLifecycleManagement.PutLifecycle(new PutLifecycleRequest("policy_id")))
					.FluentAsync(c => c.IndexLifecycleManagement.PutLifecycleAsync("policy_id"))
					.RequestAsync(c => c.IndexLifecycleManagement.PutLifecycleAsync(new PutLifecycleRequest("policy_id")))
				;

			await POST("/index/_ilm/remove")
					.Fluent(c => c.IndexLifecycleManagement.RemovePolicy("index"))
					.Request(c => c.IndexLifecycleManagement.RemovePolicy(new RemovePolicyRequest("index")))
					.FluentAsync(c => c.IndexLifecycleManagement.RemovePolicyAsync("index"))
					.RequestAsync(c => c.IndexLifecycleManagement.RemovePolicyAsync(new RemovePolicyRequest("index")))
				;

			await POST("/index/_ilm/retry")
					.Fluent(c => c.IndexLifecycleManagement.RetryIlm("index"))
					.Request(c => c.IndexLifecycleManagement.RetryIlm(new RetryIlmRequest("index")))
					.FluentAsync(c => c.IndexLifecycleManagement.RetryIlmAsync("index"))
					.RequestAsync(c => c.IndexLifecycleManagement.RetryIlmAsync(new RetryIlmRequest("index")))
				;

			await POST("/_ilm/start")
					.Fluent(c => c.IndexLifecycleManagement.StartIlm())
					.Request(c => c.IndexLifecycleManagement.StartIlm())
					.FluentAsync(c => c.IndexLifecycleManagement.StartIlmAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.StartIlmAsync())
				;

			await POST("/_ilm/stop")
					.Fluent(c => c.IndexLifecycleManagement.StopIlm())
					.Request(c => c.IndexLifecycleManagement.StopIlm())
					.FluentAsync(c => c.IndexLifecycleManagement.StopIlmAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.StopIlmAsync())
				;
		}
	}
}
