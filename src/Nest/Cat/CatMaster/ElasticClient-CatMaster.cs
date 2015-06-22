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
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null)
		{
			return this.DoCat<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector,
				this.RawDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);
		}

		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request)
		{
			return this.DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request,
				this.RawDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);
		}

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(
			Func<CatMasterDescriptor, CatMasterDescriptor> selector = null)
		{
			return this.DoCatAsync<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector,
				this.RawDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
		}

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)
		{
			return this.DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request,
				this.RawDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
		}
	}
}
		