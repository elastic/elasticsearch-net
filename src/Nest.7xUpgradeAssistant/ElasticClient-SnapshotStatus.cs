using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.Status(), please update this usage.")]
		public static SnapshotStatusResponse SnapshotStatus(this IElasticClient client,
			Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null
		)
			=> client.Snapshot.Status(selector);

		[Obsolete("Moved to client.Snapshot.Status(), please update this usage.")]
		public static SnapshotStatusResponse SnapshotStatus(this IElasticClient client, ISnapshotStatusRequest request)
			=> client.Snapshot.Status(request);

		[Obsolete("Moved to client.Snapshot.StatusAsync(), please update this usage.")]
		public static Task<SnapshotStatusResponse> SnapshotStatusAsync(this IElasticClient client,
			Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.StatusAsync(selector, ct);

		[Obsolete("Moved to client.Snapshot.StatusAsync(), please update this usage.")]
		public static Task<SnapshotStatusResponse> SnapshotStatusAsync(this IElasticClient client, ISnapshotStatusRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.StatusAsync(request, ct);
	}
}
