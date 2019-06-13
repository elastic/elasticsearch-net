using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Activates a currently inactive watch.
		/// </summary>
		public static ActivateWatchResponse ActivateWatch(this IElasticClient client,Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc />
		public static ActivateWatchResponse ActivateWatch(this IElasticClient client,IActivateWatchRequest request);

		/// <inheritdoc />
		public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client,Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client,IActivateWatchRequest request,
			CancellationToken ct = default
		);
	}
}
