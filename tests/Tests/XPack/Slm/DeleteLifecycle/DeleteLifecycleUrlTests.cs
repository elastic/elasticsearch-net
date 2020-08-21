// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.DeleteLifecycle
{
	public class DeleteLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await DELETE("/_slm/policy/policy_id")
				.Fluent(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle("policy_id"))
				.Request(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle(new DeleteSnapshotLifecycleRequest("policy_id")))
				.FluentAsync(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync("policy_id"))
				.RequestAsync(c => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync(new DeleteSnapshotLifecycleRequest("policy_id")));
	}
}
