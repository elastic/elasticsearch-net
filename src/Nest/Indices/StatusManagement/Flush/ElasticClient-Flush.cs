using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The flush API allows to flush one or more indices through an API. The flush process of an index basically 
		/// frees memory from the index by flushing data to the index storage and clearing the internal transaction log. 
		/// By default, Elasticsearch uses memory heuristics in order to automatically trigger 
		/// flush operations as required in order to clear memory.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-flush.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the flush operation</param>
		IShardsOperationResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null);

		/// <inheritdoc/>
		IShardsOperationResponse Flush(IFlushRequest flushRequest);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> FlushAsync(IFlushRequest flushRequest);
	}
	
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IShardsOperationResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null) =>
			this.Flush(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IShardsOperationResponse Flush(IFlushRequest flushRequest) => 
			this.Dispatcher.Dispatch<IFlushRequest, FlushRequestParameters, ShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null) => 
			this.FlushAsync(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> FlushAsync(IFlushRequest flushRequest) => 
			this.Dispatcher.DispatchAsync<IFlushRequest, FlushRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				flushRequest,
				(p, d) => this.LowLevelDispatch.IndicesFlushDispatchAsync<ShardsOperationResponse>(p)
			);
	}
}