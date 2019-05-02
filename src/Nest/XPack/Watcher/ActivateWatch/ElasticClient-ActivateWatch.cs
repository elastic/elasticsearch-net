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
		ActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc />
		ActivateWatchResponse ActivateWatch(IActivateWatchRequest request);

		/// <inheritdoc />
		Task<ActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) =>
			ActivateWatch(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)));

		/// <inheritdoc />
		public ActivateWatchResponse ActivateWatch(IActivateWatchRequest request) =>
			DoRequest<IActivateWatchRequest, ActivateWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ActivateWatchResponse> ActivateWatchAsync(
			Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		) => ActivateWatchAsync(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<ActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IActivateWatchRequest, ActivateWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
