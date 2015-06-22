using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse Flush(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<FlushDescriptor, FlushRequestParameters, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse Flush(IFlushRequest flushRequest)
		{
			return this.Dispatcher.Dispatch<IFlushRequest, FlushRequestParameters, ShardsOperationResponse>(
				flushRequest,
				(p, d) => this.RawDispatch.IndicesFlushDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> FlushAsync(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<FlushDescriptor, FlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> FlushAsync(IFlushRequest flushRequest)
		{
			return this.Dispatcher.DispatchAsync<IFlushRequest, FlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				flushRequest,
				(p, d) => this.RawDispatch.IndicesFlushDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse SyncedFlush(Func<SyncedFlushDescriptor, SyncedFlushDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<SyncedFlushDescriptor, SyncedFlushRequestParameters, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushSyncedDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse SyncedFlush(ISyncedFlushRequest flushRequest)
		{
			return this.Dispatcher.Dispatch<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse>(
				flushRequest,
				(p, d) => this.RawDispatch.IndicesFlushSyncedDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> SyncedFlushAsync(Func<SyncedFlushDescriptor, SyncedFlushDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<SyncedFlushDescriptor, SyncedFlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushSyncedDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> SyncedFlushAsync(ISyncedFlushRequest flushRequest)
		{
			return this.Dispatcher.DispatchAsync<ISyncedFlushRequest, SyncedFlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				flushRequest,
				(p, d) => this.RawDispatch.IndicesFlushSyncedDispatchAsync<ShardsOperationResponse>(p)
			);
		}
	}
}