using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// A repository can contain multiple snapshots of the same cluster. Snapshot are identified by unique names within the
		/// cluster.
		/// ///
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The name of the repository we want to create a snapshot in</param>
		/// <param name="snapshotName">The name of the snapshot</param>
		/// <param name="selector">Optionally provide more details about the snapshot operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SnapshotResponse Snapshot(this IElasticClient client, Name repository, Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null
		)
			=> client.Snapshot.Snapshot(repository, snapshotName, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SnapshotResponse Snapshot(this IElasticClient client, ISnapshotRequest request)
			=> client.Snapshot.Snapshot(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SnapshotResponse> SnapshotAsync(this IElasticClient client, Name repository, Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.SnapshotAsync(repository, snapshotName, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SnapshotResponse> SnapshotAsync(this IElasticClient client, ISnapshotRequest request, CancellationToken ct = default)
			=> client.Snapshot.SnapshotAsync(request, ct);
	}
}
