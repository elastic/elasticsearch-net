using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.Restore(), please update this usage.")]
		public static RestoreResponse Restore(this IElasticClient client, Name repository, Name snapshotName,
			Func<RestoreDescriptor, IRestoreRequest> selector = null
		)
			=> client.Snapshot.Restore(repository, snapshotName, selector);

		[Obsolete("Moved to client.Snapshot.Restore(), please update this usage.")]
		public static RestoreResponse Restore(this IElasticClient client, IRestoreRequest request)
			=> client.Snapshot.Restore(request);

		[Obsolete("Moved to client.Snapshot.RestoreAsync(), please update this usage.")]
		public static Task<RestoreResponse> RestoreAsync(this IElasticClient client, Name repository, Name snapshotName,
			Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.RestoreAsync(repository, snapshotName, selector, ct);

		[Obsolete("Moved to client.Snapshot.RestoreAsync(), please update this usage.")]
		public static Task<RestoreResponse> RestoreAsync(this IElasticClient client, IRestoreRequest request, CancellationToken ct = default)
			=> client.Snapshot.RestoreAsync(request, ct);
	}
}
