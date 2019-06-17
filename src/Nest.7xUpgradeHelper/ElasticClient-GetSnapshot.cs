using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets information about one or more snapshots
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshots live</param>
		/// <param name="snapshots">The names of the snapshots we want information from (can be _all or wildcards)</param>
		/// <param name="selector">Optionally further describe the get snapshot operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetSnapshotResponse GetSnapshot(this IElasticClient client, Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null
		)
			=> client.Snapshot.Get(repository, snapshots, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetSnapshotResponse GetSnapshot(this IElasticClient client, IGetSnapshotRequest request)
			=> client.Snapshot.Get(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client, Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken ct = default
		)
			=> client.Snapshot.GetAsync(repository, snapshots, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client, IGetSnapshotRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.GetAsync(request, ct);
	}
}
