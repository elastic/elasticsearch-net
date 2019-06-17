using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Restore a snapshot
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_restore
		/// </summary>
		/// <param name="repository">The repository name that holds our snapshot</param>
		/// <param name="snapshotName">The name of the snapshot that we want to restore</param>
		/// <param name="selector">Optionally further describe the restore operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RestoreResponse Restore(this IElasticClient client, Name repository, Name snapshotName,
			Func<RestoreDescriptor, IRestoreRequest> selector = null
		)
			=> client.Snapshot.Restore(repository, snapshotName, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RestoreResponse Restore(this IElasticClient client, IRestoreRequest request)
			=> client.Snapshot.Restore(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RestoreResponse> RestoreAsync(this IElasticClient client, Name repository, Name snapshotName,
			Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.RestoreAsync(repository, snapshotName, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RestoreResponse> RestoreAsync(this IElasticClient client, IRestoreRequest request, CancellationToken ct = default)
			=> client.Snapshot.RestoreAsync(request, ct);
	}
}
