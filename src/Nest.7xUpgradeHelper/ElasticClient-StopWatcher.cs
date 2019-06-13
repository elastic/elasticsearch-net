using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops the Watcher/Alerting service, if the service is running
		/// </summary>
		public static StopWatcherResponse StopWatcher(this IElasticClient client,Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null);

		/// <inheritdoc />
		public static StopWatcherResponse StopWatcher(this IElasticClient client,IStopWatcherRequest request);

		/// <inheritdoc />
		public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client,Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client,IStopWatcherRequest request, CancellationToken ct = default);
	}

}
