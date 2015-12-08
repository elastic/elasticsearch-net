using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The optimize API allows to optimize one or more indices through an API. The optimize process basically optimizes 
		/// the index for faster search operations (and relates to the number of segments a Lucene index holds within each shard).
		///  The optimize operation allows to reduce the number of segments by merging them.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-optimize.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-optimize.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor that further describes the optimize operation, i.e limit it to one index</param>
		IShardsOperationResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null);

		/// <inheritdoc/>
		IShardsOperationResponse Optimize(IOptimizeRequest request);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> OptimizeAsync(IOptimizeRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IShardsOperationResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null) =>
			this.Optimize(selector.InvokeOrDefault(new OptimizeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IShardsOperationResponse Optimize(IOptimizeRequest request) => 
			this.Dispatcher.Dispatch<IOptimizeRequest, OptimizeRequestParameters, ShardsOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOptimizeDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null) => 
			this.OptimizeAsync(selector.InvokeOrDefault(new OptimizeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> OptimizeAsync(IOptimizeRequest request) => 
			this.Dispatcher.DispatchAsync<IOptimizeRequest, OptimizeRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOptimizeDispatchAsync<ShardsOperationResponse>(p)
			);
	}
}