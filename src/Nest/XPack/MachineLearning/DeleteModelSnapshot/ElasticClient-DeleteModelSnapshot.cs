using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an existing machine learning model snapshot.
		/// </summary>
		/// <remarks>
		/// You cannot delete the active model snapshot, unless you first revert to a different one.
		/// </remarks>
		IDeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null);

		/// <inheritdoc/>
		IDeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request);

		/// <inheritdoc/>
		Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null) =>
			this.DeleteModelSnapshot(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc/>
		public IDeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request) =>
			this.Dispatcher.Dispatch<IDeleteModelSnapshotRequest, DeleteModelSnapshotRequestParameters, DeleteModelSnapshotResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlDeleteModelSnapshotDispatch<DeleteModelSnapshotResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteModelSnapshotAsync(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteModelSnapshotRequest, DeleteModelSnapshotRequestParameters, DeleteModelSnapshotResponse, IDeleteModelSnapshotResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlDeleteModelSnapshotDispatchAsync<DeleteModelSnapshotResponse>(p, c)
			);
	}
}
