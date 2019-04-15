﻿using System;
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
		DeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		DeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request);

		/// <inheritdoc />
		Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteModelSnapshotResponse DeleteModelSnapshot(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null
		) => DeleteModelSnapshot(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public DeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request) =>
			DoRequest<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => DeleteModelSnapshotAsync(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId, snapshotId)), ct);

		/// <inheritdoc />
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse, DeleteModelSnapshotResponse>(request, request.RequestParameters, ct);
	}
}
