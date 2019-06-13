using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static GetSnapshotResponse GetSnapshot(this IElasticClient client,Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null);

		/// <inheritdoc />
		public static GetSnapshotResponse GetSnapshot(this IElasticClient client,IGetSnapshotRequest request);

		/// <inheritdoc />
		public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client,Name repository, Names snapshots,
			Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetSnapshotResponse> GetSnapshotAsync(this IElasticClient client,IGetSnapshotRequest request, CancellationToken ct = default);
	}

}
