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
		SnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc />
		SnapshotResponse Snapshot(ISnapshotRequest request);

		/// <inheritdoc />
		Task<SnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<SnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SnapshotResponse Snapshot(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) =>
			Snapshot(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public SnapshotResponse Snapshot(ISnapshotRequest request) =>
			DoRequest<ISnapshotRequest, SnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SnapshotResponse> SnapshotAsync(
			Name repository,
			Name snapshotName,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null,
			CancellationToken ct = default
		) =>
			SnapshotAsync(selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName)), ct);

		/// <inheritdoc />
		public Task<SnapshotResponse> SnapshotAsync(ISnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISnapshotRequest, SnapshotResponse>(request, request.RequestParameters, ct);
	}
}
