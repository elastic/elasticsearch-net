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
		IUpdateModelSnapshotResponse UpdateModelSnapshot(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		IUpdateModelSnapshotResponse UpdateModelSnapshot(IUpdateModelSnapshotRequest request);

		/// <inheritdoc />
		Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateModelSnapshotResponse UpdateModelSnapshot(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		) =>
			UpdateModelSnapshot(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public IUpdateModelSnapshotResponse UpdateModelSnapshot(IUpdateModelSnapshotRequest request) =>
			Dispatch2<IUpdateModelSnapshotRequest, UpdateModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => UpdateModelSnapshotAsync(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId, snapshotId)), ct);

		/// <inheritdoc />
		public Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IUpdateModelSnapshotRequest, IUpdateModelSnapshotResponse, UpdateModelSnapshotResponse>
				(request, request.RequestParameters, ct);
	}
}
