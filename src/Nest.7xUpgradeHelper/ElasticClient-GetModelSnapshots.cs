using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetModelSnapshots(), please update this usage.")]
		public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client, Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null
		)
			=> client.MachineLearning.GetModelSnapshots(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetModelSnapshots(), please update this usage.")]
		public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client, IGetModelSnapshotsRequest request)
			=> client.MachineLearning.GetModelSnapshots(request);

		[Obsolete("Moved to client.MachineLearning.GetModelSnapshotsAsync(), please update this usage.")]
		public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client, Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetModelSnapshotsAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetModelSnapshotsAsync(), please update this usage.")]
		public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client, IGetModelSnapshotsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetModelSnapshotsAsync(request, ct);
	}
}
