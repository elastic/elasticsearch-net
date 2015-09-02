using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Restore a snapshot
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_restore
		/// </summary>
		/// <param name="repository">The repository name that holds our snapshot</param>
		/// <param name="snapshotName">The name of the snapshot that we want to restore</param>
		/// <param name="selector">Optionally further describe the restore operation</param>
		IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc/>
		IRestoreResponse Restore(IRestoreRequest restoreRequest);

		/// <inheritdoc/>
		Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc/>
		Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRestoreResponse Restore(IRestoreRequest restoreRequest) => 
			this.Dispatcher.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				restoreRequest,
				this.LowLevelDispatch.SnapshotRestoreDispatch<RestoreResponse>
			);

		/// <inheritdoc/>
		public IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null) => 
			this.Dispatcher.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				selector(new RestoreDescriptor().Snapshot(snapshotName).Repository(repository)),
				this.LowLevelDispatch.SnapshotRestoreDispatch<RestoreResponse>
			);

		/// <inheritdoc/>
		public Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest) => 
			this.Dispatcher.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				restoreRequest,
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null) => 
			this.Dispatcher.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				selector(new RestoreDescriptor().Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.LowLevelDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
	}
}