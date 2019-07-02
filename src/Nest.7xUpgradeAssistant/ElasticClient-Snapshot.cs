using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.Snapshot(), please update this usage.")]
		public static SnapshotResponse Snapshot(this IElasticClient client, Name repository, Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null
		)
			=> client.Snapshot.Snapshot(repository, snapshotName, selector);

		[Obsolete("Moved to client.Snapshot.Snapshot(), please update this usage.")]
		public static SnapshotResponse Snapshot(this IElasticClient client, ISnapshotRequest request)
			=> client.Snapshot.Snapshot(request);

		[Obsolete("Moved to client.Snapshot.SnapshotAsync(), please update this usage.")]
		public static Task<SnapshotResponse> SnapshotAsync(this IElasticClient client, Name repository, Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.SnapshotAsync(repository, snapshotName, selector, ct);

		[Obsolete("Moved to client.Snapshot.SnapshotAsync(), please update this usage.")]
		public static Task<SnapshotResponse> SnapshotAsync(this IElasticClient client, ISnapshotRequest request, CancellationToken ct = default)
			=> client.Snapshot.SnapshotAsync(request, ct);
	}
}
