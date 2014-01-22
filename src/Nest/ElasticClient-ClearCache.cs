using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector)
		{
			return this.Dispatch<ClearCacheDescriptor, ClearCacheQueryString, IndicesResponse>(
				selector,
				(p, d)=> this.RawDispatch.IndicesClearCacheDispatch(p)
			);
		}
		
		public Task<IIndicesResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector)
		{
			return this.DispatchAsync<ClearCacheDescriptor, ClearCacheQueryString, IndicesResponse, IIndicesResponse>(
				selector,
				(p, d)=> this.RawDispatch.IndicesClearCacheDispatchAsync(p)
			);
		}
		
	}
}
