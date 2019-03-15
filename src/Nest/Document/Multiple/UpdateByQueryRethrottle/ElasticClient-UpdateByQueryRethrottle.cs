using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Rethrottles a running update by query. Rethrottling that speeds up the query takes effect immediately
		/// but rethrotting that slows down the query will take effect after completing the current batch. This prevents scroll timeouts.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-update-by-query.html#docs-update-by-query-rethrottle">https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-update-by-query.html#docs-update-by-query-rethrottle</a>
		/// </summary>
		IListTasksResponse UpdateByQueryRethrottle(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		IListTasksResponse UpdateByQueryRethrottle(IUpdateByQueryRethrottleRequest request);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		Task<IListTasksResponse> UpdateByQueryRethrottleAsync(TaskId taskId,
			Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		Task<IListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

 	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IListTasksResponse UpdateByQueryRethrottle(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null) =>
			UpdateByQueryRethrottle(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)));

 		/// <inheritdoc />
		public IListTasksResponse UpdateByQueryRethrottle(IUpdateByQueryRethrottleRequest request) =>
			Dispatcher.Dispatch<IUpdateByQueryRethrottleRequest, UpdateByQueryRethrottleRequestParameters, ListTasksResponse>(
				request,
				(p, d) => LowLevelDispatch.UpdateByQueryRethrottleDispatch<ListTasksResponse>(p)
			);

 		/// <inheritdoc />
		public Task<IListTasksResponse> UpdateByQueryRethrottleAsync(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			UpdateByQueryRethrottleAsync(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)), cancellationToken);

 		/// <inheritdoc />
		public Task<IListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IUpdateByQueryRethrottleRequest, UpdateByQueryRethrottleRequestParameters, ListTasksResponse, IListTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.UpdateByQueryRethrottleDispatchAsync<ListTasksResponse>(p, c)
			);
	}
}
