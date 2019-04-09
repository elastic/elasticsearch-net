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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector) =>
			ExecuteWatch(selector.InvokeOrDefault(new ExecuteWatchDescriptor()));

		/// <inheritdoc />
		public IExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request) =>
			Dispatch2<IExecuteWatchRequest, ExecuteWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(
			Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		) => ExecuteWatchAsync(selector?.InvokeOrDefault(new ExecuteWatchDescriptor()), ct);

		/// <inheritdoc />
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IExecuteWatchRequest, IExecuteWatchResponse, ExecuteWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
