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
		public ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null) =>
			this.DoCat<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.LowLevelDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);

		public ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request) =>
			this.DoCat<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.LowLevelDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null) =>
			this.DoCatAsync<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.LowLevelDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request) =>
			this.DoCatAsync<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.LowLevelDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);

	}
}