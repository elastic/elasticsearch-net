using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Activates a currently inactive watch.
		/// </summary>
		IActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc />
		IActivateWatchResponse ActivateWatch(IActivateWatchRequest request);

		/// <inheritdoc />
		Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) =>
			ActivateWatch(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)));

		/// <inheritdoc />
		public IActivateWatchResponse ActivateWatch(IActivateWatchRequest request) =>
			DoRequest<IActivateWatchRequest, ActivateWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IActivateWatchResponse> ActivateWatchAsync(
			Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		) => ActivateWatchAsync(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IActivateWatchRequest, IActivateWatchResponse, ActivateWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
