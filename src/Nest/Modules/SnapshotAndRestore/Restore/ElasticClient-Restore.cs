using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRestoreResponse Restore(IRestoreRequest restoreRequest)
		{
			return this.Dispatcher.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				restoreRequest,
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<RestoreDescriptor, RestoreRequestParameters, RestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest)
		{
			return this.Dispatcher.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				restoreRequest,
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<RestoreDescriptor, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

	}
}