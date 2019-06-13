using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Delete a snapshot
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshot we want to delete lives</param>
		/// <param name="snapshotName">The name of the snapshot that we want to delete</param>
		/// <param name="selector">Optionally further describe the delete snapshot operation</param>
		public static DeleteSnapshotResponse DeleteSnapshot(this IElasticClient client,Name repository, Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		public static DeleteSnapshotResponse DeleteSnapshot(this IElasticClient client,IDeleteSnapshotRequest request);

		/// <inheritdoc />
		public static Task<DeleteSnapshotResponse> DeleteSnapshotAsync(this IElasticClient client,
			Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteSnapshotResponse> DeleteSnapshotAsync(this IElasticClient client,IDeleteSnapshotRequest request,
			CancellationToken ct = default
		);
	}

}
