using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves information about machine learning model snapshots.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client, Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null
		)
			=> client.MachineLearning.GetModelSnapshots(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client, IGetModelSnapshotsRequest request)
			=> client.MachineLearning.GetModelSnapshots(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client, Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetModelSnapshotsAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client, IGetModelSnapshotsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetModelSnapshotsAsync(request, ct);
	}
}
