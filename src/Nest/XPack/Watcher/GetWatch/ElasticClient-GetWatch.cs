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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) =>
			GetWatch(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)));

		/// <inheritdoc />
		public IGetWatchResponse GetWatch(IGetWatchRequest request) =>
			Dispatcher.Dispatch<IGetWatchRequest, GetWatchRequestParameters, GetWatchResponse>(
				request,
				(p, d) => LowLevelDispatch.WatcherGetWatchDispatch<GetWatchResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetWatchAsync(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetWatchRequest, GetWatchRequestParameters, GetWatchResponse, IGetWatchResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.WatcherGetWatchDispatchAsync<GetWatchResponse>(p, c)
			);
	}
}
