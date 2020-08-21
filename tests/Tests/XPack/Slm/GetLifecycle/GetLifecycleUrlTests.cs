// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.GetLifecycle
{
	public class GetLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
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
		}
	}
}
