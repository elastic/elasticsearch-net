using System;
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

		/// <inheritdoc/>
		ISyncedFlushResponse SyncedFlush(ISyncedFlushRequest request);

		/// <inheritdoc/>
		Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc/>
		Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISyncedFlushResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) =>
			this.SyncedFlush(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public ISyncedFlushResponse SyncedFlush(ISyncedFlushRequest request) => 
			this.Dispatcher.Dispatch<ISyncedFlushRequest, SyncedFlushRequestParameters, SyncedFlushResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatch<SyncedFlushResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) => 
			this.SyncedFlushAsync(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request) => 
			this.Dispatcher.DispatchAsync<ISyncedFlushRequest, SyncedFlushRequestParameters, SyncedFlushResponse, ISyncedFlushResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatchAsync<SyncedFlushResponse>(p)
			);
	}
}