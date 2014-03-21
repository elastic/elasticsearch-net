using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<ClearCacheDescriptor, ClearCacheQueryString, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClearCacheDescriptor, ClearCacheQueryString, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
		}
	}
}