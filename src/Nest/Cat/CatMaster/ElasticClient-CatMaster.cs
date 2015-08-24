using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null) =>
			this.DoCat<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector,
				this.LowLevelDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);

		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request) =>
			this.DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request,
				this.LowLevelDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null) =>
			this.DoCatAsync<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector,
				this.LowLevelDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request) =>
			this.DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request,
				this.LowLevelDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
	}
}
		