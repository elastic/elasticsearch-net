using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Acknowledges a watch, to manually throttle execution of the watch's actions
		/// while the watch condition remains <c>true</c>.
		/// An acknowledged watch action remains in the acknowledged (acked) state until the watch’s condition
		/// evaluates to <c>false</c>.
		/// </summary>
		public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client,Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null);

		/// <inheritdoc />
		public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client,IAcknowledgeWatchRequest request);

		/// <inheritdoc />
		public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client,Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client,IAcknowledgeWatchRequest request,
			CancellationToken ct = default
		);
	}
}
