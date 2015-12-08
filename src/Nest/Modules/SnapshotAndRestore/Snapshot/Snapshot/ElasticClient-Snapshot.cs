using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// A repository can contain multiple snapshots of the same cluster. Snapshot are identified by unique names within the cluster.
		/// /// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The name of the repository we want to create a snapshot in</param>
		/// <param name="snapshotName">The name of the snapshot</param>
		/// <param name="selector">Optionally provide more details about the snapshot operation</param>
		ISnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc/>
		ISnapshotResponse Snapshot(ISnapshotRequest request);

		/// <inheritdoc/>
		Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc/>
		Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) =>
			this.Snapshot(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(ISnapshotRequest request) => 
			this.Dispatcher.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				request,
				this.LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>
			);

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) => 
			this.SnapshotAsync(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request) => 
			this.Dispatcher.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				request,
				this.LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>
			);
	}
}