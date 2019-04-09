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
		IDeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		IDeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request);

		/// <inheritdoc />
		Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteModelSnapshotResponse DeleteModelSnapshot(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		) => DeleteModelSnapshot(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public IDeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request) =>
			Dispatch2<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => DeleteModelSnapshotAsync(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)), ct);

		/// <inheritdoc />
		public Task<IDeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteModelSnapshotRequest, IDeleteModelSnapshotResponse, DeleteModelSnapshotResponse>(request, request.RequestParameters, ct);
	}
}
