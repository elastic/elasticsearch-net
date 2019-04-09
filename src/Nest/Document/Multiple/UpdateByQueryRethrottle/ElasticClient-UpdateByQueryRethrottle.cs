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
			CancellationToken ct = default
		);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		Task<IListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request,
			CancellationToken ct = default
		);
	}

 	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IListTasksResponse UpdateByQueryRethrottle(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null) =>
			UpdateByQueryRethrottle(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)));

 		/// <inheritdoc />
		public IListTasksResponse UpdateByQueryRethrottle(IUpdateByQueryRethrottleRequest request) =>
			Dispatch2<IUpdateByQueryRethrottleRequest, ListTasksResponse>(request, request.RequestParameters);

 		/// <inheritdoc />
		public Task<IListTasksResponse> UpdateByQueryRethrottleAsync(
			TaskId taskId,
			Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		) => UpdateByQueryRethrottleAsync(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)), ct);

 		/// <inheritdoc />
		public Task<IListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IUpdateByQueryRethrottleRequest, IListTasksResponse, ListTasksResponse>(request, request.RequestParameters, ct);
	}
}
