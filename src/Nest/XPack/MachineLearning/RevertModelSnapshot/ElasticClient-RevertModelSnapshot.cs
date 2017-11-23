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
		IRevertModelSnapshotResponse RevertModelSnapshot(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null);

		/// <inheritdoc/>
		IRevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request);

		/// <inheritdoc/>
		Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRevertModelSnapshotResponse RevertModelSnapshot(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null) =>
			this.RevertModelSnapshot(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)));

		/// <inheritdoc/>
		public IRevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request) =>
			this.Dispatcher.Dispatch<IRevertModelSnapshotRequest, RevertModelSnapshotRequestParameters, RevertModelSnapshotResponse>(
				request,
				this.LowLevelDispatch.XpackMlRevertModelSnapshotDispatch<RevertModelSnapshotResponse>
			);

		/// <inheritdoc/>
		public Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.RevertModelSnapshotAsync(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId, snapshotId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IRevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IRevertModelSnapshotRequest, RevertModelSnapshotRequestParameters, RevertModelSnapshotResponse, IRevertModelSnapshotResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlRevertModelSnapshotDispatchAsync<RevertModelSnapshotResponse>
			);
	}
}
