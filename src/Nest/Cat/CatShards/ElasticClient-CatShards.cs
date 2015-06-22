using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null)
		{
			return this.DoCat<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.RawDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);
		}

		public ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request)
		{
			return this.DoCat<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.RawDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);
		}

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null)
		{
			return this.DoCatAsync<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.RawDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);
		}

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)
		{
			return this.DoCatAsync<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.RawDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);
		}

	}
}