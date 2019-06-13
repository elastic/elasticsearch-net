using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve information about the tasks currently executing on one or more nodes in the cluster.
		/// </summary>
		/// <param name="selector">A descriptor to further describe the tasks to retrieve information for</param>
		public static ListTasksResponse ListTasks(this IElasticClient client,Func<ListTasksDescriptor, IListTasksRequest> selector = null);

		/// <inheritdoc />
		public static ListTasksResponse ListTasks(this IElasticClient client,IListTasksRequest request);

		/// <inheritdoc />
		public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client,Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client,IListTasksRequest request, CancellationToken ct = default);
	}

}
