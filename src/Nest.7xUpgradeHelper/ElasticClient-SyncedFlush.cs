using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static SyncedFlushResponse SyncedFlush(this IElasticClient client,Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc />
		public static SyncedFlushResponse SyncedFlush(this IElasticClient client,ISyncedFlushRequest request);

		/// <inheritdoc />
		public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client,Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<SyncedFlushResponse> SyncedFlushAsync(this IElasticClient client,ISyncedFlushRequest request, CancellationToken ct = default);
	}

}
