using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		
		public IShardsOperationResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null)
		{
			refreshSelector = refreshSelector ?? (s => s);
			return this.Dispatch<RefreshDescriptor, RefreshQueryString, ShardsOperationResponse>(
				refreshSelector,
				(p,d) => this.RawDispatch.IndicesRefreshDispatch(p)
			);
		}
		
		public Task<IShardsOperationResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null)
		{
			refreshSelector = refreshSelector ?? (s => s);
			return this.DispatchAsync<RefreshDescriptor, RefreshQueryString, ShardsOperationResponse, IShardsOperationResponse>(
				refreshSelector,
				(p,d)=> this.RawDispatch.IndicesRefreshDispatchAsync(p)
			);
		}
		

	}
}
