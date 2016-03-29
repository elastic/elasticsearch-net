using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null) =>
			this.CatThreadPool(selector.InvokeOrDefault(new CatThreadPoolDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request) =>
			this.DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.LowLevelDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null) =>
			this.CatThreadPoolAsync(selector.InvokeOrDefault(new CatThreadPoolDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request) =>
			this.DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.LowLevelDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
	}
}