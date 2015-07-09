using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null)
		{
			return this.Dispatcher.Dispatch<SnapshotStatusDescriptor, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest getSnapshotRequest)
		{
			return this.Dispatcher.Dispatch<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				getSnapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null)
		{
			return this.Dispatcher.DispatchAsync<SnapshotStatusDescriptor, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest getSnapshotRequest)
		{
			return this.Dispatcher.DispatchAsync<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				getSnapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p)
			);
		}

	}
}