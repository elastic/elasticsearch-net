using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.Delete(), please update this usage.")]
		public static DeleteSnapshotResponse DeleteSnapshot(this IElasticClient client, Name repository, Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		)
			=> client.Snapshot.Delete(repository, snapshotName, selector);

		[Obsolete("Moved to client.Snapshot.Delete(), please update this usage.")]
		public static DeleteSnapshotResponse DeleteSnapshot(this IElasticClient client, IDeleteSnapshotRequest request)
			=> client.Snapshot.Delete(request);

		[Obsolete("Moved to client.Snapshot.DeleteAsync(), please update this usage.")]
		public static Task<DeleteSnapshotResponse> DeleteSnapshotAsync(this IElasticClient client,
			Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteAsync(repository, snapshotName, selector, ct);

		[Obsolete("Moved to client.Snapshot.DeleteAsync(), please update this usage.")]
		public static Task<DeleteSnapshotResponse> DeleteSnapshotAsync(this IElasticClient client, IDeleteSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteAsync(request, ct);
	}
}
