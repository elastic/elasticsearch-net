using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null) =>
			this.CatPendingTasks(selector.InvokeOrDefault(new CatPendingTasksDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request) =>
			this.DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.LowLevelDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null) =>
			this.CatPendingTasksAsync(selector.InvokeOrDefault(new CatPendingTasksDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request) =>
			this.DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.LowLevelDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
	}
}