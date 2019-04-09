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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			DeactivateWatch(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc />
		public IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request) =>
			Dispatch2<IDeactivateWatchRequest, DeactivateWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(
			Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		) => DeactivateWatchAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeactivateWatchRequest, IDeactivateWatchResponse, DeactivateWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
