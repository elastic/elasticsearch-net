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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) =>
			Snapshot(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public ISnapshotResponse Snapshot(ISnapshotRequest request) =>
			Dispatch2<ISnapshotRequest, SnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ISnapshotResponse> SnapshotAsync(
			Name repository,
			Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken ct = default
		) =>
			SnapshotAsync(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)), ct);

		/// <inheritdoc />
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken ct = default) =>
			Dispatch2Async<ISnapshotRequest, ISnapshotResponse, SnapshotResponse>(request, request.RequestParameters, ct);
	}
}
