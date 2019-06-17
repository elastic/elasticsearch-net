using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an existing machine learning model snapshot.
		/// </summary>
		/// <remarks>
		/// You cannot delete the active model snapshot, unless you first revert to a different one.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client, Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		)
			=> client.MachineLearning.DeleteModelSnapshot(jobId, snapshotId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client, IDeleteModelSnapshotRequest request)
			=> client.MachineLearning.DeleteModelSnapshot(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client, Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteModelSnapshotAsync(jobId, snapshotId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client, IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteModelSnapshotAsync(request);
	}
}
