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
		IRevertModelSnapshotResponse RevertModelSnapshot(Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		IRevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request);

		/// <inheritdoc />
		Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRevertModelSnapshotResponse RevertModelSnapshot(
			Id jobId,
			Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null
		) => RevertModelSnapshot(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc />
		public IRevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request) =>
			DoRequest<IRevertModelSnapshotRequest, RevertModelSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(
			Id jobId,
			Id snapshotId,
			Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null,
			CancellationToken cancellationToken = default
		) => RevertModelSnapshotAsync(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)), cancellationToken);

		/// <inheritdoc />
		public Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRevertModelSnapshotRequest, IRevertModelSnapshotResponse, RevertModelSnapshotResponse>
				(request, request.RequestParameters, ct);
	}
}
