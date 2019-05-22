using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Api.Cat;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null) =>
			CatPendingTasks(selector.InvokeOrDefault(new CatPendingTasksDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request) =>
			DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default
		) => CatPendingTasksAsync(selector.InvokeOrDefault(new CatPendingTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, ct);
	}
}
