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
			return this.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				restoreRequest,
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<RestoreDescriptor, RestoreRequestParameters, RestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest)
		{
			return this.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				restoreRequest,
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<RestoreDescriptor, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest)
		{
			restoreRequest.ThrowIfNull("restoreRequest");
			var observable = new RestoreObservable(this, restoreRequest);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> restoreSelector = null)
		{
			restoreSelector.ThrowIfNull("restoreSelector");

			var restoreDescriptor = restoreSelector(new RestoreDescriptor());
			var observable = new RestoreObservable(this, restoreDescriptor);
			return observable;
		}
	}
}