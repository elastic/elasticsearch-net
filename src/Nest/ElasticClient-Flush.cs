using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesOperationResponse Flush(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.Dispatch<FlushDescriptor, FlushQueryString, IndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatch(p)
			);
		}
		
		public Task<IIndicesOperationResponse> FlushAsync(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.DispatchAsync<FlushDescriptor, FlushQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatchAsync(p)
			);
		}

	}
}
