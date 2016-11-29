using System;
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

		/// <inheritdoc/>
		IExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request);

		/// <inheritdoc/>
		Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector);

		/// <inheritdoc/>
		Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExecuteWatchResponse ExecuteWatch(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector) =>
			this.ExecuteWatch(selector.InvokeOrDefault(new ExecuteWatchDescriptor()));

		/// <inheritdoc/>
		public IExecuteWatchResponse ExecuteWatch(IExecuteWatchRequest request) =>
			this.Dispatcher.Dispatch<IExecuteWatchRequest, ExecuteWatchRequestParameters, ExecuteWatchResponse>(
				request,
				this.LowLevelDispatch.WatcherExecuteWatchDispatch<ExecuteWatchResponse>
			);

		/// <inheritdoc/>
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector) =>
			this.ExecuteWatchAsync(selector?.InvokeOrDefault(new ExecuteWatchDescriptor()));

		/// <inheritdoc/>
		public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IExecuteWatchRequest, ExecuteWatchRequestParameters, ExecuteWatchResponse, IExecuteWatchResponse>(
				request,
				this.LowLevelDispatch.WatcherExecuteWatchDispatchAsync<ExecuteWatchResponse>
			);
	}
}
