using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Clears the cache for the given indices, if no index is specified will clear cache of ALL indices
		/// </summary>
		/// <param name="selector">defaults to clearing all the caches on all indices</param>
		public IIndicesResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<ClearCacheDescriptor, ClearCacheQueryString, IndicesResponse>(
				selector,
				(p, d)=> this.RawDispatch.IndicesClearCacheDispatch(p)
			);
		}
		
		/// <summary>
		/// Clears the cache for the given indices, if no index is specified will clear cache of ALL indices
		/// </summary>
		/// <param name="selector">defaults to clearing all the caches on all indices</param>
		public Task<IIndicesResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClearCacheDescriptor, ClearCacheQueryString, IndicesResponse, IIndicesResponse>(
				selector,
				(p, d)=> this.RawDispatch.IndicesClearCacheDispatchAsync(p)
			);
		}
		
	}
}
