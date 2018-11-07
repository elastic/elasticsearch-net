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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null) =>
			SnapshotStatus(selector.InvokeOrDefault(new SnapshotStatusDescriptor()));

		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request) =>
			Dispatcher.Dispatch<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SnapshotStatusAsync(selector.InvokeOrDefault(new SnapshotStatusDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p, c)
			);
	}
}
