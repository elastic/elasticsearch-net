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
		public ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null)
		{
			return this.DoCat<CatFielddataDescriptor, CatFielddataRequestParameters, CatFielddataRecord>(selector, this.RawDispatch.CatFielddataDispatch<CatResponse<CatFielddataRecord>>);
		}

		public ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request)
		{
			return this.DoCat<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.RawDispatch.CatFielddataDispatch<CatResponse<CatFielddataRecord>>);
		}

		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null)
		{
			return this.DoCatAsync<CatFielddataDescriptor, CatFielddataRequestParameters, CatFielddataRecord>(selector, this.RawDispatch.CatFielddataDispatchAsync<CatResponse<CatFielddataRecord>>);
		}

		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)
		{
			return this.DoCatAsync<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.RawDispatch.CatFielddataDispatchAsync<CatResponse<CatFielddataRecord>>);
		}
	}
}