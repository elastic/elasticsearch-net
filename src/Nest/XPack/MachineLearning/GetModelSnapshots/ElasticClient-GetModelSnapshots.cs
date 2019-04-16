using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves information about machine learning model snapshots.
		/// </summary>
		IGetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null);

		/// <inheritdoc />
		IGetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request);

		/// <inheritdoc />
		Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null) =>
			GetModelSnapshots(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request) =>
			Dispatcher.Dispatch<IGetModelSnapshotsRequest, GetModelSnapshotsRequestParameters, GetModelSnapshotsResponse>(
				request,
				LowLevelDispatch.MlGetModelSnapshotsDispatch<GetModelSnapshotsResponse>
			);

		/// <inheritdoc />
		public Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetModelSnapshotsAsync(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetModelSnapshotsRequest, GetModelSnapshotsRequestParameters, GetModelSnapshotsResponse, IGetModelSnapshotsResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.MlGetModelSnapshotsDispatchAsync<GetModelSnapshotsResponse>
				);
	}
}
