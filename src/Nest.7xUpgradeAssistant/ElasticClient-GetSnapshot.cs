using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.Get(), please update this usage.")]
		public static GetSnapshotResponse GetSnapshot(this IElasticClient client, Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null
		)
			=> client.Snapshot.Get(repository, snapshots, selector);

		[Obsolete("Moved to client.Snapshot.Get(), please update this usage.")]
		public static GetSnapshotResponse GetSnapshot(this IElasticClient client, IGetSnapshotRequest request)
			=> client.Snapshot.Get(request);

		[Obsolete("Moved to client.Snapshot.GetAsync(), please update this usage.")]
		public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client, Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken ct = default
		)
			=> client.Snapshot.GetAsync(repository, snapshots, selector, ct);

		[Obsolete("Moved to client.Snapshot.GetAsync(), please update this usage.")]
		public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client, IGetSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetAsync(request, ct);
	}
}
