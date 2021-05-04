// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetModelSnapshots
{
	public class GetModelSnapshotsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_ml/anomaly_detectors/job_id/model_snapshots")
					.Fluent(c => c.MachineLearning.GetModelSnapshots("job_id"))
					.Request(c => c.MachineLearning.GetModelSnapshots(new GetModelSnapshotsRequest("job_id")))
					.FluentAsync(c => c.MachineLearning.GetModelSnapshotsAsync("job_id"))
					.RequestAsync(c => c.MachineLearning.GetModelSnapshotsAsync(new GetModelSnapshotsRequest("job_id")))
				;

			await POST("/_ml/anomaly_detectors/job_id/model_snapshots/snapshot_id")
					.Fluent(c => c.MachineLearning.GetModelSnapshots("job_id", r => r.SnapshotId("snapshot_id")))
					.Request(c => c.MachineLearning.GetModelSnapshots(new GetModelSnapshotsRequest("job_id", "snapshot_id")))
					.FluentAsync(c => c.MachineLearning.GetModelSnapshotsAsync("job_id", r => r.SnapshotId("snapshot_id")))
					.RequestAsync(c => c.MachineLearning.GetModelSnapshotsAsync(new GetModelSnapshotsRequest("job_id", "snapshot_id")))
				;
		}
	}
}
