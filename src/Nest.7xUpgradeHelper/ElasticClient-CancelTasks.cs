using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		public static CancelTasksResponse CancelTasks(this IElasticClient client,Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null);

		/// <inheritdoc />
		public static CancelTasksResponse CancelTasks(this IElasticClient client,ICancelTasksRequest request);

		/// <inheritdoc />
		public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client,Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client,ICancelTasksRequest request, CancellationToken ct = default);
	}
}
