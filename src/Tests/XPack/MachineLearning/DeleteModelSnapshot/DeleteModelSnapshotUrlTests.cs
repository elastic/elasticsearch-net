using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteModelSnapshot
{
	public class DeleteModelSnapshotUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/ml/anomaly_detectors/job_id/model_snapshots/snapshot_id")
				.Fluent(c => c.DeleteModelSnapshot("job_id", "snapshot_id"))
				.Request(c => c.DeleteModelSnapshot(new DeleteModelSnapshotRequest("job_id", "snapshot_id")))
				.FluentAsync(c => c.DeleteModelSnapshotAsync("job_id", "snapshot_id"))
				.RequestAsync(c => c.DeleteModelSnapshotAsync(new DeleteModelSnapshotRequest("job_id", "snapshot_id")))
				;
		}
	}
}
