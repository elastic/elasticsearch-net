using System;
using System.Collections.Generic;
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
		IShardsOperationResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc/>
		IShardsOperationResponse SyncedFlush(ISyncedFlushRequest flushRequest);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> SyncedFlushAsync(ISyncedFlushRequest flushRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IShardsOperationResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) =>
			this.SyncedFlush(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IShardsOperationResponse SyncedFlush(ISyncedFlushRequest flushRequest) => 
			this.Dispatcher.Dispatch<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) => 
			this.SyncedFlushAsync(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> SyncedFlushAsync(ISyncedFlushRequest flushRequest) => 
			this.Dispatcher.DispatchAsync<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatchAsync<ShardsOperationResponse>(p)
			);
	}
}