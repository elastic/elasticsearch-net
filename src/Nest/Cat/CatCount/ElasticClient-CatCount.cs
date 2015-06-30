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
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, CatCountDescriptor> selector = null) =>
			this.DoCat<CatCountDescriptor, CatCountRequestParameters, CatCountRecord>(selector, this.RawDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);

		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request)=>
			this.DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.RawDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);
		
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, CatCountDescriptor> selector = null) =>
			this.DoCatAsync<CatCountDescriptor, CatCountRequestParameters, CatCountRecord>(selector, this.RawDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
		
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request) =>
			this.DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.RawDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
		
	}
}