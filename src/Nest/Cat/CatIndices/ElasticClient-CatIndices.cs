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
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null)
		{
			return this.DoCat<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.RawDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);
		}

		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request)
		{
			return this.DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.RawDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);
		}

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null)
		{
			return this.DoCatAsync<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.RawDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
		}

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)
		{
			return this.DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.RawDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
		}
	}
}