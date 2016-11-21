using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Removes a watch identified by its id from Watcher. Once removed, the document representing the watch in the .watches index is gone
		/// and it will never be executed again.
		/// </summary>
		/// <remarks>
		/// Deleting a watch does not delete any watch execution records related to this watch from the watch history.
		/// </remarks>
		IDeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null);

		/// <inheritdoc/>
		IDeleteWatchResponse DeleteWatch(IDeleteWatchRequest request);

		/// <inheritdoc/>
		Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null) =>
			this.DeleteWatch(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public IDeleteWatchResponse DeleteWatch(IDeleteWatchRequest request) =>
			this.Dispatcher.Dispatch<IDeleteWatchRequest, DeleteWatchRequestParameters, DeleteWatchResponse>(
				request,
				(p,d) => this.LowLevelDispatch.WatcherDeleteWatchDispatch<DeleteWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null) =>
			this.DeleteWatchAsync(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IDeleteWatchRequest, DeleteWatchRequestParameters, DeleteWatchResponse, IDeleteWatchResponse>(
				request,
				(p,d) => this.LowLevelDispatch.WatcherDeleteWatchDispatchAsync<DeleteWatchResponse>(p)
			);
	}
}
