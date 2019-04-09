using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves a watch by its id
		/// </summary>
		IGetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null);

		/// <inheritdoc />
		IGetWatchResponse GetWatch(IGetWatchRequest request);

		/// <inheritdoc />
		Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) =>
			GetWatch(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)));

		/// <inheritdoc />
		public IGetWatchResponse GetWatch(IGetWatchRequest request) =>
			Dispatch2<IGetWatchRequest, GetWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetWatchResponse> GetWatchAsync(
			Id watchId,
			Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		) => GetWatchAsync(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetWatchRequest, IGetWatchResponse, GetWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
