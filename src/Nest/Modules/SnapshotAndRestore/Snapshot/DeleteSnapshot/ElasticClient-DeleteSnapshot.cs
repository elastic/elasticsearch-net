using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete a snapshot
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshot we want to delete lives</param>
		/// <param name="snapshotName">The name of the snapshot that we want to delete</param>
		/// <param name="selector">Optionally further describe the delete snapshot operation</param>
		DeleteSnapshotResponse DeleteSnapshot(Name repository, Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		DeleteSnapshotResponse DeleteSnapshot(IDeleteSnapshotRequest request);

		/// <inheritdoc />
		Task<DeleteSnapshotResponse> DeleteSnapshotAsync(
			Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteSnapshotResponse DeleteSnapshot(
			Name repository,
			Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		) => DeleteSnapshot(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public DeleteSnapshotResponse DeleteSnapshot(IDeleteSnapshotRequest request) =>
			DoRequest<IDeleteSnapshotRequest, DeleteSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteSnapshotResponse> DeleteSnapshotAsync(
			Name repository,
			Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => DeleteSnapshotAsync(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)), ct);

		/// <inheritdoc />
		public Task<DeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteSnapshotRequest, DeleteSnapshotResponse>(request, request.RequestParameters, ct);
	}
}
