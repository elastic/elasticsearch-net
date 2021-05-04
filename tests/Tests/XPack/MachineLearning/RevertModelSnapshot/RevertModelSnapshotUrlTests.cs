// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.RevertModelSnapshot
{
	public class RevertModelSnapshotUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/model_snapshots/snapshot_id/_revert")
			.Fluent(c => c.MachineLearning.RevertModelSnapshot("job_id", "snapshot_id"))
			.Request(c => c.MachineLearning.RevertModelSnapshot(new RevertModelSnapshotRequest("job_id", "snapshot_id")))
			.FluentAsync(c => c.MachineLearning.RevertModelSnapshotAsync("job_id", "snapshot_id"))
			.RequestAsync(c => c.MachineLearning.RevertModelSnapshotAsync(new RevertModelSnapshotRequest("job_id", "snapshot_id")));
	}
}
