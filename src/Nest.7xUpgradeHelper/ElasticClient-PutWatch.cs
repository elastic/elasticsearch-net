using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Registers a new watch in Watcher or updates an existing one.
		/// Once registered, a new document will be added to the .watches index, representing the watch,
		/// and its trigger will immediately be registered with the relevant trigger engine.
		/// </summary>
		public static PutWatchResponse PutWatch(this IElasticClient client,Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		public static PutWatchResponse PutWatch(this IElasticClient client,IPutWatchRequest request);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client,Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client,IPutWatchRequest request, CancellationToken ct = default);
	}

}
