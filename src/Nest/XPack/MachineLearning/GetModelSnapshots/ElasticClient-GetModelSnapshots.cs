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
		GetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null);

		/// <inheritdoc />
		GetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request);

		/// <inheritdoc />
		Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null) =>
			GetModelSnapshots(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)));

		/// <inheritdoc />
		public GetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request) =>
			DoRequest<IGetModelSnapshotsRequest, GetModelSnapshotsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId,
			Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => GetModelSnapshotsAsync(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetModelSnapshotsRequest, GetModelSnapshotsResponse>
				(request, request.RequestParameters, ct);
	}
}
