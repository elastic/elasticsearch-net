using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static RestoreResponse Restore(this IElasticClient client,Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc />
		public static RestoreResponse Restore(this IElasticClient client,IRestoreRequest request);

		/// <inheritdoc />
		public static Task<RestoreResponse> RestoreAsync(this IElasticClient client,Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RestoreResponse> RestoreAsync(this IElasticClient client,IRestoreRequest request, CancellationToken ct = default);
	}

}
