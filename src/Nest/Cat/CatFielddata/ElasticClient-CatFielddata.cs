using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null) =>
			this.CatFielddata(selector.InvokeOrDefault(new CatFielddataDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request) =>
			this.DoCat<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.LowLevelDispatch.CatFielddataDispatch<CatResponse<CatFielddataRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null) =>
			this.CatFielddataAsync(selector.InvokeOrDefault(new CatFielddataDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request) =>
			this.DoCatAsync<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.LowLevelDispatch.CatFielddataDispatchAsync<CatResponse<CatFielddataRecord>>);

	}
}