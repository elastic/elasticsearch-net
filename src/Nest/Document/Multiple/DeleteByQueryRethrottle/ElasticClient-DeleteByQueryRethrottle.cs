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
		IListTasksResponse DeleteByQueryRethrottle(TaskId taskId, Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		IListTasksResponse DeleteByQueryRethrottle(IDeleteByQueryRethrottleRequest request);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		Task<IListTasksResponse> DeleteByQueryRethrottleAsync(TaskId taskId,
			Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		Task<IListTasksResponse> DeleteByQueryRethrottleAsync(IDeleteByQueryRethrottleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IListTasksResponse DeleteByQueryRethrottle(TaskId taskId, Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null) =>
			DeleteByQueryRethrottle(selector.InvokeOrDefault(new DeleteByQueryRethrottleDescriptor(taskId)));

		/// <inheritdoc />
		public IListTasksResponse DeleteByQueryRethrottle(IDeleteByQueryRethrottleRequest request) =>
			Dispatcher.Dispatch<IDeleteByQueryRethrottleRequest, DeleteByQueryRethrottleRequestParameters, ListTasksResponse>(
				request,
				(p, d) => LowLevelDispatch.DeleteByQueryRethrottleDispatch<ListTasksResponse>(p)
			);

		/// <inheritdoc />
		public Task<IListTasksResponse> DeleteByQueryRethrottleAsync(TaskId taskId, Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteByQueryRethrottleAsync(selector.InvokeOrDefault(new DeleteByQueryRethrottleDescriptor(taskId)), cancellationToken);

		/// <inheritdoc />
		public Task<IListTasksResponse> DeleteByQueryRethrottleAsync(IDeleteByQueryRethrottleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteByQueryRethrottleRequest, DeleteByQueryRethrottleRequestParameters, ListTasksResponse, IListTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.DeleteByQueryRethrottleDispatchAsync<ListTasksResponse>(p, c)
			);
	}
}
