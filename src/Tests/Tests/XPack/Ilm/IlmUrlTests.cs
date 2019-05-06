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
					.Fluent(c => c.IlmDeleteLifecycle("policy_id"))
					.Request(c => c.IlmDeleteLifecycle(new IlmDeleteLifecycleRequest("policy_id")))
					.FluentAsync(c => c.IlmDeleteLifecycleAsync("policy_id"))
					.RequestAsync(c => c.IlmDeleteLifecycleAsync(new IlmDeleteLifecycleRequest("policy_id")))
				;

			await GET("/index/_ilm/explain")
					.Fluent(c => c.IlmExplainLifecycle("index"))
					.Request(c => c.IlmExplainLifecycle(new IlmExplainLifecycleRequest("index")))
					.FluentAsync(c => c.IlmExplainLifecycleAsync("index"))
					.RequestAsync(c => c.IlmExplainLifecycleAsync(new IlmExplainLifecycleRequest("index")))
				;

			await GET("/_ilm/policy")
					.Fluent(c => c.IlmGetLifecycle())
					.Request(c => c.IlmGetLifecycle(new IlmGetLifecycleRequest()))
					.FluentAsync(c => c.IlmGetLifecycleAsync())
					.RequestAsync(c => c.IlmGetLifecycleAsync(new IlmGetLifecycleRequest()))
				;

			await GET("/_ilm/status")
					.Fluent(c => c.IlmGetStatus())
					.Request(c => c.IlmGetStatus())
					.FluentAsync(c => c.IlmGetStatusAsync())
					.RequestAsync(c => c.IlmGetStatusAsync())
				;

			await POST("/_ilm/move/index")
					.Fluent(c => c.IlmMoveToStep("index"))
					.Request(c => c.IlmMoveToStep(new IlmMoveToStepRequest("index")))
					.FluentAsync(c => c.IlmMoveToStepAsync("index"))
					.RequestAsync(c => c.IlmMoveToStepAsync(new IlmMoveToStepRequest("index")))
				;

			await PUT("/_ilm/policy/policy_id")
					.Fluent(c => c.IlmPutLifecycle("policy_id"))
					.Request(c => c.IlmPutLifecycle(new IlmPutLifecycleRequest("policy_id")))
					.FluentAsync(c => c.IlmPutLifecycleAsync("policy_id"))
					.RequestAsync(c => c.IlmPutLifecycleAsync(new IlmPutLifecycleRequest("policy_id")))
				;

			await POST("/index/_ilm/remove")
					.Fluent(c => c.IlmRemovePolicy("index"))
					.Request(c => c.IlmRemovePolicy(new IlmRemovePolicyRequest("index")))
					.FluentAsync(c => c.IlmRemovePolicyAsync("index"))
					.RequestAsync(c => c.IlmRemovePolicyAsync(new IlmRemovePolicyRequest("index")))
				;

			await POST("/index/_ilm/retry")
					.Fluent(c => c.IlmRetry("index"))
					.Request(c => c.IlmRetry(new IlmRetryRequest("index")))
					.FluentAsync(c => c.IlmRetryAsync("index"))
					.RequestAsync(c => c.IlmRetryAsync(new IlmRetryRequest("index")))
				;

			await POST("/_ilm/start")
					.Fluent(c => c.IlmStart())
					.Request(c => c.IlmStart())
					.FluentAsync(c => c.IlmStartAsync())
					.RequestAsync(c => c.IlmStartAsync())
				;

			await POST("/_ilm/stop")
					.Fluent(c => c.IlmStop())
					.Request(c => c.IlmStop())
					.FluentAsync(c => c.IlmStopAsync())
					.RequestAsync(c => c.IlmStopAsync())
				;
		}
	}
}
