using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Starts the Watcher/Alerting service, if the service is not already running
		/// </summary>
		public static StartWatcherResponse StartWatcher(this IElasticClient client,Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null);

		/// <inheritdoc />
		public static StartWatcherResponse StartWatcher(this IElasticClient client,IStartWatcherRequest request);

		/// <inheritdoc />
		public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client,Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client,IStartWatcherRequest request, CancellationToken ct = default);
	}

}
