using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null);

		/// <inheritdoc/>
		ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request);

		/// <inheritdoc/>
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null) =>
			this.SnapshotStatus(selector.InvokeOrDefault(new SnapshotStatusDescriptor()));

		/// <inheritdoc/>
		public ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest request) =>
			this.Dispatcher.Dispatch<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.SnapshotStatusAsync(selector.InvokeOrDefault(new SnapshotStatusDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p, c)
			);
	}
}
