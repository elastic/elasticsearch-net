using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Snapshots(), please update this usage.")]
		public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client, Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null
		)
			=> client.Cat.Snapshots(selector);

		[Obsolete("Moved to client.Cat.Snapshots(), please update this usage.")]
		public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client, ICatSnapshotsRequest request)
			=> client.Cat.Snapshots(request);

		[Obsolete("Moved to client.Cat.SnapshotsAsync(), please update this usage.")]
		public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client,
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.SnapshotsAsync(selector, ct);

		[Obsolete("Moved to client.Cat.SnapshotsAsync(), please update this usage.")]
		public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client, ICatSnapshotsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.SnapshotsAsync(request, ct);
	}
}
