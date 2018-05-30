using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.RevertModelSnapshot
{
	public class RevertModelSnapshotUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/model_snapshots/snapshot_id/_revert")
				.Fluent(c => c.RevertModelSnapshot("job_id", "snapshot_id"))
				.Request(c => c.RevertModelSnapshot(new RevertModelSnapshotRequest("job_id", "snapshot_id")))
				.FluentAsync(c => c.RevertModelSnapshotAsync("job_id", "snapshot_id"))
				.RequestAsync(c => c.RevertModelSnapshotAsync(new RevertModelSnapshotRequest("job_id", "snapshot_id")))
				;
		}
	}
}
