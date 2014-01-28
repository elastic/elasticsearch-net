using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{

		public IIndicesOperationResponse Optimize(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null)
		{
			optimizeSelector = optimizeSelector ?? (s => s);
			return this.Dispatch<OptimizeDescriptor, OptimizeQueryString, IndicesOperationResponse>(
				optimizeSelector,
				(p,d) => this.RawDispatch.IndicesOptimizeDispatch(p)
			);
		}

		public Task<IIndicesOperationResponse> OptimizeAsync(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null)
		{
			optimizeSelector = optimizeSelector ?? (s => s);
			return this.DispatchAsync<OptimizeDescriptor, OptimizeQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				optimizeSelector,
				(p,d) => this.RawDispatch.IndicesOptimizeDispatchAsync(p)
			);

		}
	}
}
