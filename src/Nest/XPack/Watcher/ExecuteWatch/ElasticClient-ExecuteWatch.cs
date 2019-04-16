using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Forces the execution of a stored watch. It can be used to force execution of the watch outside of its triggering logic,
		/// or to simulate the watch execution for debugging purposes.
		/// </summary>
		IExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector);

		/// <inheritdoc />
		IExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request);

		/// <inheritdoc />
		Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector) =>
			ExecuteWatch(selector.InvokeOrDefault(new ExecuteWatchDescriptor()));

		/// <inheritdoc />
		public IExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request) =>
			Dispatcher.Dispatch<IExecuteWatchRequest, ExecuteWatchRequestParameters, ExecuteWatchResponse>(
				request,
				LowLevelDispatch.WatcherExecuteWatchDispatch<ExecuteWatchResponse>
			);

		/// <inheritdoc />
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ExecuteWatchAsync(selector?.InvokeOrDefault(new ExecuteWatchDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IExecuteWatchRequest, ExecuteWatchRequestParameters, ExecuteWatchResponse, IExecuteWatchResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.WatcherExecuteWatchDispatchAsync<ExecuteWatchResponse>
			);
	}
}
