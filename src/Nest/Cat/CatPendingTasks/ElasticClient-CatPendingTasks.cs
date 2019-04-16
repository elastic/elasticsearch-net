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
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null) =>
			CatPendingTasks(selector.InvokeOrDefault(new CatPendingTasksDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request) =>
			DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default
		) => CatPendingTasksAsync(selector.InvokeOrDefault(new CatPendingTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, ct);
	}
}
