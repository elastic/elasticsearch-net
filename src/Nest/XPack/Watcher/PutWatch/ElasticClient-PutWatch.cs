using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Registers a new watch in Watcher or updates an existing one.
		/// Once registered, a new document will be added to the .watches index, representing the watch,
		/// and its trigger will immediately be registered with the relevant trigger engine.
		/// </summary>
		IPutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		IPutWatchResponse PutWatch(IPutWatchRequest request);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null) =>
			PutWatch(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)));

		/// <inheritdoc />
		public IPutWatchResponse PutWatch(IPutWatchRequest request) =>
			Dispatch2<IPutWatchRequest, PutWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutWatchResponse> PutWatchAsync(
			Id watchId,
			Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		) => PutWatchAsync(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutWatchRequest, IPutWatchResponse, PutWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
