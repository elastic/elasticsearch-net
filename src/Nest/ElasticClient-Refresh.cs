using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		
		public IIndicesShardResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector)
		{
			return this.Dispatch<RefreshDescriptor, RefreshQueryString, IIndicesShardResponse>(
				refreshSelector,
				(p,d) => this.RawDispatch.IndicesRefreshDispatch(p)
			);
		}
		
		public Task<IIndicesShardResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector)
		{
			return this.DispatchAsync<RefreshDescriptor, RefreshQueryString, IndicesShardResponse, IIndicesShardResponse>(
				refreshSelector,
				(p,d)=> this.RawDispatch.IndicesRefreshDispatchAsync(p)
			);
		}
		

	}
}
