using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Rethrottles a running delete by query. Rethrottling that speeds up the query takes effect immediately
		/// but rethrotting that slows down the query will take effect after completing the current batch. This prevents scroll timeouts.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete-by-query.html#docs-delete-by-query-rethrottle">https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete-by-query.html#docs-delete-by-query-rethrottle</a>
		/// </summary>
		public static ListTasksResponse DeleteByQueryRethrottle(this IElasticClient client,TaskId taskId, Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		public static ListTasksResponse DeleteByQueryRethrottle(this IElasticClient client,IDeleteByQueryRethrottleRequest request);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		public static Task<ListTasksResponse> DeleteByQueryRethrottleAsync(this IElasticClient client,TaskId taskId,
			Func<DeleteByQueryRethrottleDescriptor, IDeleteByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteByQueryRethrottle(Nest.TaskId,System.Func{Nest.DeleteByQueryRethrottleDescriptor,Nest.IDeleteByQueryRethrottleRequest})" />
		public static Task<ListTasksResponse> DeleteByQueryRethrottleAsync(this IElasticClient client,IDeleteByQueryRethrottleRequest request,
			CancellationToken ct = default
		);
	}

}
