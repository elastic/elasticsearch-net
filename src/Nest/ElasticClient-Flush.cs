using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		public IShardsOperationResponse Flush(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.Dispatch<FlushDescriptor, FlushQueryString, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatch<ShardsOperationResponse>(p)
			);
		}
		
		public Task<IShardsOperationResponse> FlushAsync(Func<FlushDescriptor, FlushDescriptor> selector)
		{
			return this.DispatchAsync<FlushDescriptor, FlushQueryString, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesFlushDispatchAsync<ShardsOperationResponse>(p)
			);
		}

	}
}
