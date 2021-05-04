// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateModelSnapshot
{
	public class UpdateModelSnapshotUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/model_snapshots/snapshot_id/_update")
			.Fluent(c => c.MachineLearning.UpdateModelSnapshot("job_id", "snapshot_id", p => p))
			.Request(c => c.MachineLearning.UpdateModelSnapshot(new UpdateModelSnapshotRequest("job_id", "snapshot_id")))
			.FluentAsync(c => c.MachineLearning.UpdateModelSnapshotAsync("job_id", "snapshot_id", p => p))
			.RequestAsync(c => c.MachineLearning.UpdateModelSnapshotAsync(new UpdateModelSnapshotRequest("job_id", "snapshot_id")));
	}
}
