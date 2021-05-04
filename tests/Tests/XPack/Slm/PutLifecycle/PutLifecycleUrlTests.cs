// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.PutLifecycle
{
	public class PutLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await PUT("/_slm/policy/policy_id")
				.Fluent(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycle("policy_id"))
				.Request(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycle(new PutSnapshotLifecycleRequest("policy_id")))
				.FluentAsync(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync("policy_id"))
				.RequestAsync(c => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync(new PutSnapshotLifecycleRequest("policy_id")));
	}
}
