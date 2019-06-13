using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Rethrottles a running update by query. Rethrottling that speeds up the query takes effect immediately
		/// but rethrotting that slows down the query will take effect after completing the current batch. This prevents scroll timeouts.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-update-by-query.html#docs-update-by-query-rethrottle">https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-update-by-query.html#docs-update-by-query-rethrottle</a>
		/// </summary>
		public static ListTasksResponse UpdateByQueryRethrottle(this IElasticClient client,TaskId taskId, Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		public static ListTasksResponse UpdateByQueryRethrottle(this IElasticClient client,IUpdateByQueryRethrottleRequest request);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		public static Task<ListTasksResponse> UpdateByQueryRethrottleAsync(this IElasticClient client,TaskId taskId,
			Func<UpdateByQueryRethrottleDescriptor, IUpdateByQueryRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

 		/// <inheritdoc cref="UpdateByQueryRethrottle(Nest.TaskId,System.Func{Nest.UpdateByQueryRethrottleDescriptor,Nest.IUpdateByQueryRethrottleRequest})" />
		public static Task<ListTasksResponse> UpdateByQueryRethrottleAsync(this IElasticClient client,IUpdateByQueryRethrottleRequest request,
			CancellationToken ct = default
		);
	}

}
