using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Forces the execution of a stored watch. It can be used to force execution of the watch outside of its triggering logic,
		/// or to simulate the watch execution for debugging purposes.
		/// </summary>
		public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client,Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector);

		/// <inheritdoc />
		public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client,IExecuteWatchRequest request);

		/// <inheritdoc />
		public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client,Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client,IExecuteWatchRequest request, CancellationToken ct = default);
	}

}
