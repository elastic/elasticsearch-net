using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatCountRecord> CatCount(ICatCountRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			this.CatCount(selector.InvokeOrDefault(new CatCountDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request)=>
			this.DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.LowLevelDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);
		
		/// <inheritdoc/>
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			this.CatCountAsync(selector.InvokeOrDefault(new CatCountDescriptor()));
		
		/// <inheritdoc/>
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request) =>
			this.DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.LowLevelDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
		
	}
}