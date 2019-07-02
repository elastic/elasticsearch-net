using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.SyncedFlush(), please update this usage.")]
		public static SyncedFlushResponse SyncedFlush(this IElasticClient client, Indices indices,
			Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null
		)
			=> client.Indices.SyncedFlush(indices, selector);

		[Obsolete("Moved to client.Indices.SyncedFlush(), please update this usage.")]
		public static SyncedFlushResponse SyncedFlush(this IElasticClient client, ISyncedFlushRequest request)
			=> client.Indices.SyncedFlush(request);

		[Obsolete("Moved to client.Indices.SyncedFlushAsync(), please update this usage.")]
		public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client, Indices indices,
			Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SyncedFlushAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.SyncedFlushAsync(), please update this usage.")]
		public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client, ISyncedFlushRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.SyncedFlushAsync(request, ct);
	}
}
