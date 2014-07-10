using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse Optimize(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null)
		{
			optimizeSelector = optimizeSelector ?? (s => s);
			return this.Dispatch<OptimizeDescriptor, OptimizeRequestParameters, ShardsOperationResponse>(
				optimizeSelector,
				(p, d) => this.RawDispatch.IndicesOptimizeDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse Optimize(IOptimizeRequest optimizeRequest)
		{
			return this.Dispatch<IOptimizeRequest, OptimizeRequestParameters, ShardsOperationResponse>(
				optimizeRequest,
				(p, d) => this.RawDispatch.IndicesOptimizeDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> OptimizeAsync(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null)
		{
			optimizeSelector = optimizeSelector ?? (s => s);
			return this.DispatchAsync<OptimizeDescriptor, OptimizeRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				optimizeSelector,
				(p, d) => this.RawDispatch.IndicesOptimizeDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> OptimizeAsync(IOptimizeRequest optimizeRequest)
		{
			return this.DispatchAsync<IOptimizeRequest, OptimizeRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				optimizeRequest,
				(p, d) => this.RawDispatch.IndicesOptimizeDispatchAsync<ShardsOperationResponse>(p)
			);
		}

	}
}