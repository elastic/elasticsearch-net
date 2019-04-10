using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null);

		/// <inheritdoc />
		ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request);

		/// <inheritdoc />
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null) =>
			SnapshotStatus(selector.InvokeOrDefault(new SnapshotStatusDescriptor()));

		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request) =>
			DoRequest<ISnapshotStatusRequest, SnapshotStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(
			Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		) => SnapshotStatusAsync(selector.InvokeOrDefault(new SnapshotStatusDescriptor()), ct);

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISnapshotStatusRequest, ISnapshotStatusResponse, SnapshotStatusResponse>(request, request.RequestParameters, ct);
	}
}
