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
		/// <inheritdoc />
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null)
		{
			return this.DoCat<CatThreadPoolDescriptor, CatThreadPoolRequestParameters, CatThreadPoolRecord>(selector, this.RawDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);
		}

		public ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request)
		{
			return this.DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.RawDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);
		}

		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null)
		{
			return this.DoCatAsync<CatThreadPoolDescriptor, CatThreadPoolRequestParameters, CatThreadPoolRecord>(selector, this.RawDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
		}

		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)
		{
			return this.DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.RawDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
		}
	}
}