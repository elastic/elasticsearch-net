using System;
using System.Threading;
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
		DeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null);

		/// <inheritdoc />
		DeleteWatchResponse DeleteWatch(IDeleteWatchRequest request);

		/// <inheritdoc />
		Task<DeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteWatchResponse DeleteWatch(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null) =>
			DeleteWatch(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)));

		/// <inheritdoc />
		public DeleteWatchResponse DeleteWatch(IDeleteWatchRequest request) =>
			DoRequest<IDeleteWatchRequest, DeleteWatchResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteWatchResponse> DeleteWatchAsync(
			Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		) => DeleteWatchAsync(selector.InvokeOrDefault(new DeleteWatchDescriptor(watchId)), ct);

		/// <inheritdoc />
		public Task<DeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteWatchRequest, DeleteWatchResponse>
				(request, request.RequestParameters, ct);
	}
}
