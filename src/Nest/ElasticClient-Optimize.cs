using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{

		public IIndicesOperationResponse Optimize(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector)
		{
			optimizeSelector.ThrowIfNull("optimizeSelector");
			var descriptor = optimizeSelector(new OptimizeDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesOptimizeDispatch(pathInfo)
				.Deserialize<IndicesOperationResponse>();
		}

		public Task<IIndicesOperationResponse> OptimizeAsync(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector)
		{
			optimizeSelector.ThrowIfNull("optimizeSelector");
			var descriptor = optimizeSelector(new OptimizeDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);

			return this.RawDispatch.IndicesOptimizeDispatchAsync(pathInfo)
				.ContinueWith<IIndicesOperationResponse>(r => r.Result.Deserialize<IndicesOperationResponse>());

		}
	}
}
