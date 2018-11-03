using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The Synced Flush API allows an administrator to initiate a synced flush manually.
		/// This can be particularly useful for a planned (rolling) cluster restart where you
		/// can stop indexing and don’t want to wait the default 5 minutes for idle indices to
		/// be sync-flushed automatically.
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the synced flush operation</param>
		/// <returns></returns>
		ISyncedFlushResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc />
		ISyncedFlushResponse SyncedFlush(ISyncedFlushRequest request);

		/// <inheritdoc />
		Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISyncedFlushResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) =>
			SyncedFlush(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc />
		public ISyncedFlushResponse SyncedFlush(ISyncedFlushRequest request) =>
			Dispatcher.Dispatch<ISyncedFlushRequest, SyncedFlushRequestParameters, SyncedFlushResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesFlushSyncedDispatch<SyncedFlushResponse>(p)
			);

		/// <inheritdoc />
		public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SyncedFlushAsync(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ISyncedFlushRequest, SyncedFlushRequestParameters, SyncedFlushResponse, ISyncedFlushResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesFlushSyncedDispatchAsync<SyncedFlushResponse>(p, c)
			);
	}
}
