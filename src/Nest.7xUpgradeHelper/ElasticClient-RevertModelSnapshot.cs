using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Reverts a specific snapshot for a machine learning job
		/// </summary>
		public static RevertModelSnapshotResponse RevertModelSnapshot(this IElasticClient client,Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		public static RevertModelSnapshotResponse RevertModelSnapshot(this IElasticClient client,IRevertModelSnapshotRequest request);

		/// <inheritdoc />
		public static Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(this IElasticClient client,Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(this IElasticClient client,IRevertModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

}
