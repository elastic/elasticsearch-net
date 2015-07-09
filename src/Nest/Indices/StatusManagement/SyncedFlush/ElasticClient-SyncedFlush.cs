using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse SyncedFlush(Func<SyncedFlushDescriptor, SyncedFlushDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<SyncedFlushDescriptor, SyncedFlushRequestParameters, ShardsOperationResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse SyncedFlush(ISyncedFlushRequest flushRequest)
		{
			return this.Dispatcher.Dispatch<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> SyncedFlushAsync(Func<SyncedFlushDescriptor, SyncedFlushDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<SyncedFlushDescriptor, SyncedFlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> SyncedFlushAsync(ISyncedFlushRequest flushRequest)
		{
			return this.Dispatcher.DispatchAsync<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushSyncedDispatchAsync<ShardsOperationResponse>(p)
			);
		}
	}
}