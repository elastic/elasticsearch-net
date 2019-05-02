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
		ListTasksResponse UpdateByQueryRethrottle(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		ListTasksResponse UpdateByQueryRethrottle(IUpdateByQueryRethrottleRequest request);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		Task<ListTasksResponse> UpdateByQueryRethrottleAsync(TaskId taskId,
			Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		Task<ListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request,
			CancellationToken ct = default
		);
	}

 	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ListTasksResponse UpdateByQueryRethrottle(TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null) =>
			UpdateByQueryRethrottle(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)));

 		/// <inheritdoc />
		public ListTasksResponse UpdateByQueryRethrottle(IUpdateByQueryRethrottleRequest request) =>
			DoRequest<IUpdateByQueryRethrottleRequest, ListTasksResponse>(request, request.RequestParameters);

 		/// <inheritdoc />
		public Task<ListTasksResponse> UpdateByQueryRethrottleAsync(
			TaskId taskId,
			Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		) => UpdateByQueryRethrottleAsync(selector.InvokeOrDefault(new UpdateByQueryRethrottleDescriptor(taskId)), ct);

 		/// <inheritdoc />
		public Task<ListTasksResponse> UpdateByQueryRethrottleAsync(IUpdateByQueryRethrottleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateByQueryRethrottleRequest, ListTasksResponse>(request, request.RequestParameters, ct);
	}
}
