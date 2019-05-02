using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		SnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null);

		/// <inheritdoc />
		SnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request);

		/// <inheritdoc />
		Task<SnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<SnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null) =>
			SnapshotStatus(selector.InvokeOrDefault(new SnapshotStatusDescriptor()));

		/// <inheritdoc />
		public SnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request) =>
			DoRequest<ISnapshotStatusRequest, SnapshotStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SnapshotStatusResponse> SnapshotStatusAsync(
			Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		) => SnapshotStatusAsync(selector.InvokeOrDefault(new SnapshotStatusDescriptor()), ct);

		/// <inheritdoc />
		public Task<SnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISnapshotStatusRequest, SnapshotStatusResponse>(request, request.RequestParameters, ct);
	}
}
