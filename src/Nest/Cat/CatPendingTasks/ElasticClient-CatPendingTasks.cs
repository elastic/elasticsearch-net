using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null) =>
			CatPendingTasks(selector.InvokeOrDefault(new CatPendingTasksDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request) =>
			DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request,
				LowLevelDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatPendingTasksAsync(selector.InvokeOrDefault(new CatPendingTasksDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, cancellationToken,
				LowLevelDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
	}
}
