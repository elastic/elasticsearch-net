using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteModelSnapshot(), please update this usage.")]
		public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client, Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		)
			=> client.MachineLearning.DeleteModelSnapshot(jobId, snapshotId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteModelSnapshot(), please update this usage.")]
		public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client, IDeleteModelSnapshotRequest request)
			=> client.MachineLearning.DeleteModelSnapshot(request);

		[Obsolete("Moved to client.MachineLearning.DeleteModelSnapshotAsync(), please update this usage.")]
		public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client, Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteModelSnapshotAsync(jobId, snapshotId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteModelSnapshotAsync(), please update this usage.")]
		public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client, IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteModelSnapshotAsync(request, ct);
	}
}
