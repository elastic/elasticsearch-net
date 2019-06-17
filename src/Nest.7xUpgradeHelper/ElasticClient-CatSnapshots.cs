using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client, Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null
		)
			=> client.Cat.Snapshots(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client, ICatSnapshotsRequest request)
			=> client.Cat.Snapshots(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client,
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.SnapshotsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client, ICatSnapshotsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.SnapshotsAsync(request, ct);
	}

}
