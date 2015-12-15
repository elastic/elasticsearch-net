using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null) =>
			this.CatHealth(selector.InvokeOrDefault(new CatHealthDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request) =>
			DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, LowLevelDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector = null) => 
			this.CatHealthAsync(selector.InvokeOrDefault(new CatHealthDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request) => 
			DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, LowLevelDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
	}
}