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
		public ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null)
		{
			return this.DoCat<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, this.RawDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);
		}

		public ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request)
		{
			return this.DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, this.RawDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);
		}

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null)
		{
			return this.DoCatAsync<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, this.RawDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
		}

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)
		{
			return this.DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, this.RawDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
		}

	}
}