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
		public ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null) => 
			DoCat<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, LowLevelDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);

		public ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request) =>
			DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, LowLevelDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null) => 
			DoCatAsync<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, LowLevelDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request) => 
			DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, LowLevelDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
	}
}