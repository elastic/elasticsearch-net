using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteModelSnapshot
{
	public class DeleteModelSnapshotUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/anomaly_detectors/job_id/model_snapshots/snapshot_id")
			.Fluent(c => c.MachineLearning.DeleteModelSnapshot("job_id", "snapshot_id"))
			.Request(c => c.MachineLearning.DeleteModelSnapshot(new DeleteModelSnapshotRequest("job_id", "snapshot_id")))
			.FluentAsync(c => c.MachineLearning.DeleteModelSnapshotAsync("job_id", "snapshot_id"))
			.RequestAsync(c => c.MachineLearning.DeleteModelSnapshotAsync(new DeleteModelSnapshotRequest("job_id", "snapshot_id")));
	}
}
