using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete a snapshot
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshot we want to delete lives</param>
		/// <param name="snapshotName">The name of the snapshot that we want to delete</param>
		/// <param name="selector">Optionally further describe the delete snapshot operation</param>
		IAcknowledgedResponse DeleteSnapshot(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteSnapshot(IDeleteSnapshotRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteSnapshot(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null) =>
			this.DeleteSnapshot(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteSnapshot(IDeleteSnapshotRequest request) => 
			this.Dispatcher.Dispatch<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null) => 
			this.DeleteSnapshotAsync(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}