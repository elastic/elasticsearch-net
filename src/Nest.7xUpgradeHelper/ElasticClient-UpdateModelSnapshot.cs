using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Updates a machine learning model snapshot.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client, Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		)
			=> client.MachineLearning.UpdateModelSnapshot(jobId, snapshotId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client, IUpdateModelSnapshotRequest request)
			=> client.MachineLearning.UpdateModelSnapshot(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client, Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateModelSnapshotAsync(jobId, snapshotId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client, IUpdateModelSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateModelSnapshotAsync(request, ct);
	}
}
