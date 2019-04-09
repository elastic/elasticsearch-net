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
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null) =>
			GetModelSnapshots(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request) =>
			Dispatch2<IGetModelSnapshotsRequest, GetModelSnapshotsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => GetModelSnapshotsAsync(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetModelSnapshotsRequest, IGetModelSnapshotsResponse, GetModelSnapshotsResponse>
				(request, request.RequestParameters, ct);
	}
}
