using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets information about one or more snapshots
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshots live</param>
		/// <param name="snapshots">The names of the snapshots we want information from (can be _all or wildcards)</param>
		/// <param name="selector">Optionally further describe the get snapshot operation</param>
		IGetSnapshotResponse GetSnapshot(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null);

		/// <inheritdoc/>
		IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest request);

		/// <inheritdoc/>
		Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetSnapshotResponse GetSnapshot(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null) =>
			this.GetSnapshot(selector.InvokeOrDefault(new GetSnapshotDescriptor(repository, snapshots)));

		/// <inheritdoc/>
		public IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest request) =>
			this.Dispatcher.Dispatch<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotGetDispatch<GetSnapshotResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetSnapshotAsync(selector.InvokeOrDefault(new GetSnapshotDescriptor(repository, snapshots)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse, IGetSnapshotResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.SnapshotGetDispatchAsync<GetSnapshotResponse>(p, c)
			);
	}
}
