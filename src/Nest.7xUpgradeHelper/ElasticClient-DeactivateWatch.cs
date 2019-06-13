using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deactivates a currently active watch.
		/// </summary>
		public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client,Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null);

		/// <inheritdoc />
		public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client,IDeactivateWatchRequest request);

		/// <inheritdoc />
		public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client,Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client,IDeactivateWatchRequest request,
			CancellationToken ct = default
		);
	}

}
