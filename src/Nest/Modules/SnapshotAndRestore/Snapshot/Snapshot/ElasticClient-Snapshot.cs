using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// A repository can contain multiple snapshots of the same cluster. Snapshot are identified by unique names within the cluster.
		/// ///
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The name of the repository we want to create a snapshot in</param>
		/// <param name="snapshotName">The name of the snapshot</param>
		/// <param name="selector">Optionally provide more details about the snapshot operation</param>
		ISnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc />
		ISnapshotResponse Snapshot(ISnapshotRequest request);

		/// <inheritdoc />
		Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) =>
			Snapshot(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public ISnapshotResponse Snapshot(ISnapshotRequest request) =>
			Dispatcher.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				request,
				LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>
			);

		/// <inheritdoc />
		public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SnapshotAsync(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)), cancellationToken);

		/// <inheritdoc />
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>
			);
	}
}
