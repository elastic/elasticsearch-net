using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateModelSnapshot
{
	public class UpdateModelSnapshotUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/model_snapshots/snapshot_id/_update")
				.Fluent(c => c.UpdateModelSnapshot("job_id", "snapshot_id", p => p))
				.Request(c => c.UpdateModelSnapshot(new UpdateModelSnapshotRequest("job_id", "snapshot_id")))
				.FluentAsync(c => c.UpdateModelSnapshotAsync("job_id", "snapshot_id", p => p))
				.RequestAsync(c => c.UpdateModelSnapshotAsync(new UpdateModelSnapshotRequest("job_id", "snapshot_id")))
				;
		}
	}
}
