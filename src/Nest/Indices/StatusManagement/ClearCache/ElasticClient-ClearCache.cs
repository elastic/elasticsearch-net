using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{

	public partial interface IElasticClient
	{
		/// <summary>
		/// The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-clearcache.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the clear cache operation</param>
		IShardsOperationResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc/>
		IShardsOperationResponse ClearCache(IClearCacheRequest clearCacheRequest);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> ClearCacheAsync(IClearCacheRequest clearCacheRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IShardsOperationResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null) => 
			this.Dispatcher.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse>(
				selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)),
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public IShardsOperationResponse ClearCache(IClearCacheRequest clearCacheRequest) => 
			this.Dispatcher.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null) => this.Dispatcher.DispatchAsync<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)),
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> ClearCacheAsync(IClearCacheRequest clearCacheRequest) => 
			this.Dispatcher.DispatchAsync<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
	}
}