using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.UpdateModelSnapshot(), please update this usage.")]
		public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client, Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		)
			=> client.MachineLearning.UpdateModelSnapshot(jobId, snapshotId, selector);

		[Obsolete("Moved to client.MachineLearning.UpdateModelSnapshot(), please update this usage.")]
		public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client, IUpdateModelSnapshotRequest request)
			=> client.MachineLearning.UpdateModelSnapshot(request);

		[Obsolete("Moved to client.MachineLearning.UpdateModelSnapshotAsync(), please update this usage.")]
		public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client, Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateModelSnapshotAsync(jobId, snapshotId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.UpdateModelSnapshotAsync(), please update this usage.")]
		public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client, IUpdateModelSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateModelSnapshotAsync(request, ct);
	}
}
