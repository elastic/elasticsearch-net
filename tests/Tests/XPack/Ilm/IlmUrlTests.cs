// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

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
					.Fluent(c => c.IndexLifecycleManagement.GetStatus())
					.Request(c => c.IndexLifecycleManagement.GetStatus())
					.FluentAsync(c => c.IndexLifecycleManagement.GetStatusAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.GetStatusAsync())
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
					.Fluent(c => c.IndexLifecycleManagement.Retry("index"))
					.Request(c => c.IndexLifecycleManagement.Retry(new RetryIlmRequest("index")))
					.FluentAsync(c => c.IndexLifecycleManagement.RetryAsync("index"))
					.RequestAsync(c => c.IndexLifecycleManagement.RetryAsync(new RetryIlmRequest("index")))
				;

			await POST("/_ilm/start")
					.Fluent(c => c.IndexLifecycleManagement.Start())
					.Request(c => c.IndexLifecycleManagement.Start())
					.FluentAsync(c => c.IndexLifecycleManagement.StartAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.StartAsync())
				;

			await POST("/_ilm/stop")
					.Fluent(c => c.IndexLifecycleManagement.Stop())
					.Request(c => c.IndexLifecycleManagement.Stop())
					.FluentAsync(c => c.IndexLifecycleManagement.StopAsync())
					.RequestAsync(c => c.IndexLifecycleManagement.StopAsync())
				;
		}
	}
}
