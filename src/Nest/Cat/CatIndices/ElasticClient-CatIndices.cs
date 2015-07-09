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
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null) =>
			this.DoCat<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.LowLevelDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);

		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request) =>
			this.DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.LowLevelDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null) =>
			this.DoCatAsync<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.LowLevelDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request) =>
			this.DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.LowLevelDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
	}
}