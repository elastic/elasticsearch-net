using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Reverts a specific snapshot for a machine learning job
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RevertModelSnapshotResponse RevertModelSnapshot(this IElasticClient client, Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		)
			=> client.MachineLearning.RevertModelSnapshot(jobId, snapshotId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RevertModelSnapshotResponse RevertModelSnapshot(this IElasticClient client, IRevertModelSnapshotRequest request)
			=> client.MachineLearning.RevertModelSnapshot(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(this IElasticClient client, Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.RevertModelSnapshotAsync(jobId, snapshotId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(this IElasticClient client, IRevertModelSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.RevertModelSnapshotAsync(request, ct);
	}
}
