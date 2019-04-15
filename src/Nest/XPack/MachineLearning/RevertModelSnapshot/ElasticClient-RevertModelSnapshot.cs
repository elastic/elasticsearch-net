using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Reverts a specific snapshot for a machine learning job
		/// </summary>
		RevertModelSnapshotResponse RevertModelSnapshot(Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		RevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request);

		/// <inheritdoc />
		Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RevertModelSnapshotResponse RevertModelSnapshot(
			Id jobId,
			Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		) => RevertModelSnapshot(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public RevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request) =>
			DoRequest<IRevertModelSnapshotRequest, RevertModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken cancellationToken = default
		) => RevertModelSnapshotAsync(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)), cancellationToken);

		/// <inheritdoc />
		public Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRevertModelSnapshotRequest, RevertModelSnapshotResponse, RevertModelSnapshotResponse>
				(request, request.RequestParameters, ct);
	}
}
