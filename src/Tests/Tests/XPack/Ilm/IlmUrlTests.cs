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
					.Fluent(c => c.DeleteLifecycle("policy_id"))
					.Request(c => c.DeleteLifecycle(new DeleteLifecycleRequest("policy_id")))
					.FluentAsync(c => c.DeleteLifecycleAsync("policy_id"))
					.RequestAsync(c => c.DeleteLifecycleAsync(new DeleteLifecycleRequest("policy_id")))
				;

			await GET("/index/_ilm/explain")
					.Fluent(c => c.ExplainLifecycle("index"))
					.Request(c => c.ExplainLifecycle(new ExplainLifecycleRequest("index")))
					.FluentAsync(c => c.ExplainLifecycleAsync("index"))
					.RequestAsync(c => c.ExplainLifecycleAsync(new ExplainLifecycleRequest("index")))
				;

			await GET("/_ilm/policy")
					.Fluent(c => c.GetLifecycle())
					.Request(c => c.GetLifecycle(new GetLifecycleRequest()))
					.FluentAsync(c => c.GetLifecycleAsync())
					.RequestAsync(c => c.GetLifecycleAsync(new GetLifecycleRequest()))
				;

			await GET("/_ilm/status")
					.Fluent(c => c.GetIlmStatus())
					.Request(c => c.GetIlmStatus())
					.FluentAsync(c => c.GetIlmStatusAsync())
					.RequestAsync(c => c.GetIlmStatusAsync())
				;

			await POST("/_ilm/move/index")
					.Fluent(c => c.MoveToStep("index"))
					.Request(c => c.MoveToStep(new MoveToStepRequest("index")))
					.FluentAsync(c => c.MoveToStepAsync("index"))
					.RequestAsync(c => c.MoveToStepAsync(new MoveToStepRequest("index")))
				;

			await PUT("/_ilm/policy/policy_id")
					.Fluent(c => c.PutLifecycle("policy_id"))
					.Request(c => c.PutLifecycle(new PutLifecycleRequest("policy_id")))
					.FluentAsync(c => c.PutLifecycleAsync("policy_id"))
					.RequestAsync(c => c.PutLifecycleAsync(new PutLifecycleRequest("policy_id")))
				;

			await POST("/index/_ilm/remove")
					.Fluent(c => c.RemovePolicy("index"))
					.Request(c => c.RemovePolicy(new RemovePolicyRequest("index")))
					.FluentAsync(c => c.RemovePolicyAsync("index"))
					.RequestAsync(c => c.RemovePolicyAsync(new RemovePolicyRequest("index")))
				;

			await POST("/index/_ilm/retry")
					.Fluent(c => c.RetryIlm("index"))
					.Request(c => c.RetryIlm(new RetryIlmRequest("index")))
					.FluentAsync(c => c.RetryIlmAsync("index"))
					.RequestAsync(c => c.RetryIlmAsync(new RetryIlmRequest("index")))
				;

			await POST("/_ilm/start")
					.Fluent(c => c.StartIlm())
					.Request(c => c.StartIlm())
					.FluentAsync(c => c.StartIlmAsync())
					.RequestAsync(c => c.StartIlmAsync())
				;

			await POST("/_ilm/stop")
					.Fluent(c => c.StopIlm())
					.Request(c => c.StopIlm())
					.FluentAsync(c => c.StopIlmAsync())
					.RequestAsync(c => c.StopIlmAsync())
				;
		}
	}
}
