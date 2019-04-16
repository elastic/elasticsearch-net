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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
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
			Dispatcher.Dispatch<IUpdateModelSnapshotRequest, UpdateModelSnapshotRequestParameters, UpdateModelSnapshotResponse>(
				request,
				LowLevelDispatch.MlUpdateModelSnapshotDispatch<UpdateModelSnapshotResponse>
			);

		/// <inheritdoc />
		public Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			UpdateModelSnapshotAsync(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId, snapshotId)), cancellationToken);

		/// <inheritdoc />
		public Task<IUpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IUpdateModelSnapshotRequest, UpdateModelSnapshotRequestParameters, UpdateModelSnapshotResponse,
					IUpdateModelSnapshotResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.MlUpdateModelSnapshotDispatchAsync<UpdateModelSnapshotResponse>
				);
	}
}
