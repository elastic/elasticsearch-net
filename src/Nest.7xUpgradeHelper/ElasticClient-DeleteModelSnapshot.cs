using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client,Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		public static DeleteModelSnapshotResponse DeleteModelSnapshot(this IElasticClient client,IDeleteModelSnapshotRequest request);

		/// <inheritdoc />
		public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client,Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(this IElasticClient client,IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

}
