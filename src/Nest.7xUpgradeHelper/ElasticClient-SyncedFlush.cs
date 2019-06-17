using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The Synced Flush API allows an administrator to initiate a synced flush manually.
		/// This can be particularly useful for a planned (rolling) cluster restart where you
		/// can stop indexing and don’t want to wait the default 5 minutes for idle indices to
		/// be sync-flushed automatically.
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the synced flush operation</param>
		/// <returns></returns>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SyncedFlushResponse SyncedFlush(this IElasticClient client, Indices indices,
			Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null
		)
			=> client.Indices.SyncedFlush(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SyncedFlushResponse SyncedFlush(this IElasticClient client, ISyncedFlushRequest request)
			=> client.Indices.SyncedFlush(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client, Indices indices,
			Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SyncedFlushAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client, ISyncedFlushRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.SyncedFlushAsync(request);
	}
}
