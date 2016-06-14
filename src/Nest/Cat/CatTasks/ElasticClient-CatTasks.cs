using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(Func<CatTasksDescriptor, ICatTasksRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null) =>
			this.CatTasks(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request) =>
			this.DoCat<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(
				request,
				this.LowLevelDispatch.CatTasksDispatch<CatResponse<CatTasksRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(Func<CatTasksDescriptor, ICatTasksRequest> selector = null) =>
			this.CatTasksAsync(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request) =>
			this.DoCatAsync<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(
				request,
				this.LowLevelDispatch.CatTasksDispatchAsync<CatResponse<CatTasksRecord>>);
	}
}
