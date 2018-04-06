using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetModelSnapshots
{
	public class GetModelSnapshotsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/model_snapshots")
				.Fluent(c => c.GetModelSnapshots("job_id"))
				.Request(c => c.GetModelSnapshots(new GetModelSnapshotsRequest("job_id")))
				.FluentAsync(c => c.GetModelSnapshotsAsync("job_id"))
				.RequestAsync(c => c.GetModelSnapshotsAsync(new GetModelSnapshotsRequest("job_id")))
				;

			await POST("/_xpack/ml/anomaly_detectors/job_id/model_snapshots/snapshot_id")
				.Fluent(c => c.GetModelSnapshots("job_id", r => r.SnapshotId("snapshot_id")))
				.Request(c => c.GetModelSnapshots(new GetModelSnapshotsRequest("job_id", "snapshot_id")))
				.FluentAsync(c => c.GetModelSnapshotsAsync("job_id", r => r.SnapshotId("snapshot_id")))
				.RequestAsync(c => c.GetModelSnapshotsAsync(new GetModelSnapshotsRequest("job_id", "snapshot_id")))
				;
		}
	}
}
