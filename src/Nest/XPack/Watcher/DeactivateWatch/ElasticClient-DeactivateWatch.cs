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

		/// <inheritdoc/>
		IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request);

		/// <inheritdoc/>
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			this.DeactivateWatch(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc/>
		public IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request) =>
			this.Dispatcher.Dispatch<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackWatcherDeactivateWatchDispatch<DeactivateWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeactivateWatchAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse, IDeactivateWatchResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackWatcherDeactivateWatchDispatchAsync<DeactivateWatchResponse>(p, c)
			);
	}
}
