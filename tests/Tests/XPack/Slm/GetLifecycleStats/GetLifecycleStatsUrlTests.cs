// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.GetLifecycleStats
{
	public class GetLifecycleStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_slm/stats")
				.Fluent(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats())
				.Request(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats(new GetSnapshotLifecycleStatsRequest()))
				.FluentAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync())
				.RequestAsync(c => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync(new GetSnapshotLifecycleStatsRequest()));
	}
}
