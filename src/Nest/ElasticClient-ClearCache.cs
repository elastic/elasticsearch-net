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
			return this.Dispatch<ClearCacheDescriptor, ClearCacheRequestParameters, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse ClearCache(IClearCacheRequest clearCacheRequest)
		{
			return this.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClearCacheDescriptor, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> ClearCacheAsync(IClearCacheRequest clearCacheRequest)
		{
			return this.DispatchAsync<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
		}

	}
}