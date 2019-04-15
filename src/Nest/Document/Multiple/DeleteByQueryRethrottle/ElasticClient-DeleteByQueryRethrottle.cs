using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Rethrottles a running delete by query. Rethrottling that speeds up the query takes effect immediately
		/// but rethrotting that slows down the query will take effect after completing the current batch. This prevents scroll timeouts.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete-by-query.html#docs-delete-by-query-rethrottle">https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete-by-query.html#docs-delete-by-query-rethrottle</a>
		/// </summary>
		ListTasksResponse DeleteByQueryRethrottle(TaskId taskId, Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		ListTasksResponse DeleteByQueryRethrottle(IDeleteByQueryRethrottleRequest request);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		Task<ListTasksResponse> DeleteByQueryRethrottleAsync(TaskId taskId,
			Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		Task<ListTasksResponse> DeleteByQueryRethrottleAsync(IDeleteByQueryRethrottleRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ListTasksResponse DeleteByQueryRethrottle(
			TaskId taskId,
			Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null
		) => DeleteByQueryRethrottle(selector.InvokeOrDefault(new DeleteByQueryRethrottleDescriptor(taskId)));

		/// <inheritdoc />
		public ListTasksResponse DeleteByQueryRethrottle(IDeleteByQueryRethrottleRequest request) =>
			DoRequest<IDeleteByQueryRethrottleRequest, ListTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ListTasksResponse> DeleteByQueryRethrottleAsync(
			TaskId taskId,
			Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		) => DeleteByQueryRethrottleAsync(selector.InvokeOrDefault(new DeleteByQueryRethrottleDescriptor(taskId)), ct);

		/// <inheritdoc />
		public Task<ListTasksResponse> DeleteByQueryRethrottleAsync(IDeleteByQueryRethrottleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteByQueryRethrottleRequest, ListTasksResponse>(request, request.RequestParameters, ct);
	}
}
