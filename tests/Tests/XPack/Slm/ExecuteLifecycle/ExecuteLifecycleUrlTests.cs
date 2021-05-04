// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.ExecuteLifecycle
{
	public class ExecuteLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_slm/policy/policy_id/_execute")
				.Fluent(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle("policy_id"))
				.Request(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle(new ExecuteSnapshotLifecycleRequest("policy_id")))
				.FluentAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync("policy_id"))
				.RequestAsync(c => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync(new ExecuteSnapshotLifecycleRequest("policy_id")));
	}
}
