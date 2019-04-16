using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deactivates a currently active watch.
		/// </summary>
		IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null);

		/// <inheritdoc />
		IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request);

		/// <inheritdoc />
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			DeactivateWatch(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc />
		public IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request) =>
			Dispatcher.Dispatch<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse>(
				request,
				(p, d) => LowLevelDispatch.WatcherDeactivateWatchDispatch<DeactivateWatchResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeactivateWatchAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse, IDeactivateWatchResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.WatcherDeactivateWatchDispatchAsync<DeactivateWatchResponse>(p, c)
			);
	}
}
