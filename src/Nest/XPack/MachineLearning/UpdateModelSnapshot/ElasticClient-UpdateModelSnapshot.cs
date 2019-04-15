using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Updates a machine learning model snapshot.
		/// </summary>
		UpdateModelSnapshotResponse UpdateModelSnapshot(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		UpdateModelSnapshotResponse UpdateModelSnapshot(IUpdateModelSnapshotRequest request);

		/// <inheritdoc />
		Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateModelSnapshotResponse UpdateModelSnapshot(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		) =>
			UpdateModelSnapshot(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public UpdateModelSnapshotResponse UpdateModelSnapshot(IUpdateModelSnapshotRequest request) =>
			DoRequest<IUpdateModelSnapshotRequest, UpdateModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => UpdateModelSnapshotAsync(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId, snapshotId)), ct);

		/// <inheritdoc />
		public Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateModelSnapshotRequest, UpdateModelSnapshotResponse, UpdateModelSnapshotResponse>
				(request, request.RequestParameters, ct);
	}
}
