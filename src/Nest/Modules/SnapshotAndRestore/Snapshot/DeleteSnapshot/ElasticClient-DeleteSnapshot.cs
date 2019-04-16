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
		IDeleteSnapshotResponse DeleteSnapshot(Name repository, Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		IDeleteSnapshotResponse DeleteSnapshot(IDeleteSnapshotRequest request);

		/// <inheritdoc />
		Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(
			Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteSnapshotResponse DeleteSnapshot(
			Name repository,
			Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null
		) => DeleteSnapshot(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public IDeleteSnapshotResponse DeleteSnapshot(IDeleteSnapshotRequest request) =>
			DoRequest<IDeleteSnapshotRequest, DeleteSnapshotResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(
			Name repository,
			Name snapshotName,
			Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector = null,
			CancellationToken ct = default
		) => DeleteSnapshotAsync(selector.InvokeOrDefault(new DeleteSnapshotDescriptor(repository, snapshotName)), ct);

		/// <inheritdoc />
		public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteSnapshotRequest, IDeleteSnapshotResponse, DeleteSnapshotResponse>(request, request.RequestParameters, ct);
	}
}
