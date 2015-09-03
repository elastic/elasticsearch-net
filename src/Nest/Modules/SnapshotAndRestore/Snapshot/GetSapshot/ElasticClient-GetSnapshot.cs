using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets information about one or more snapshots
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshots live</param>
		/// <param name="snapshotName">The names of the snapshots we want information from (can be _all or wildcards)</param>
		/// <param name="selector">Optionally further describe the get snapshot operation</param>
		IGetSnapshotResponse GetSnapshot(string repository, string snapshotName, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null);

		/// <inheritdoc/>
		IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest getSnapshotRequest);

		/// <inheritdoc/>
		Task<IGetSnapshotResponse> GetSnapshotAsync(string repository, string snapshotName, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest getSnapshotRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetSnapshotResponse GetSnapshot(string repository, string snapshotName, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null) =>
			this.GetSnapshot(selector.InvokeOrDefault(new GetSnapshotDescriptor().Snapshot(snapshotName).Repository(repository)));

		/// <inheritdoc/>
		public IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest getSnapshotRequest) => 
			this.Dispatcher.Dispatch<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse>(
				getSnapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotGetDispatch<GetSnapshotResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetSnapshotResponse> GetSnapshotAsync(string repository, string snapshotName, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null) => 
			this.GetSnapshotAsync(selector.InvokeOrDefault(new GetSnapshotDescriptor().Snapshot(snapshotName).Repository(repository)));

		/// <inheritdoc/>
		public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest getSnapshotRequest) => 
			this.Dispatcher.DispatchAsync<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse, IGetSnapshotResponse>(
				getSnapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotGetDispatchAsync<GetSnapshotResponse>(p)
			);
	}
}