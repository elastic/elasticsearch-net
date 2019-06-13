using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves information about machine learning model snapshots.
		/// </summary>
		public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client,Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null);

		/// <inheritdoc />
		public static GetModelSnapshotsResponse GetModelSnapshots(this IElasticClient client,IGetModelSnapshotsRequest request);

		/// <inheritdoc />
		public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client,Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(this IElasticClient client,IGetModelSnapshotsRequest request,
			CancellationToken ct = default
		);
	}

}
