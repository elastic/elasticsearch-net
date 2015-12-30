using System;
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
		IOptimizeResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null);

		/// <inheritdoc/>
		IOptimizeResponse Optimize(IOptimizeRequest request);

		/// <inheritdoc/>
		Task<IOptimizeResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null);

		/// <inheritdoc/>
		Task<IOptimizeResponse> OptimizeAsync(IOptimizeRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IOptimizeResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null) =>
			this.Optimize(selector.InvokeOrDefault(new OptimizeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IOptimizeResponse Optimize(IOptimizeRequest request) => 
			this.Dispatcher.Dispatch<IOptimizeRequest, OptimizeRequestParameters, OptimizeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOptimizeDispatch<OptimizeResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IOptimizeResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector = null) => 
			this.OptimizeAsync(selector.InvokeOrDefault(new OptimizeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IOptimizeResponse> OptimizeAsync(IOptimizeRequest request) => 
			this.Dispatcher.DispatchAsync<IOptimizeRequest, OptimizeRequestParameters, OptimizeResponse, IOptimizeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOptimizeDispatchAsync<OptimizeResponse>(p)
			);
	}
}