using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Restore a snapshot
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_restore
		/// </summary>
		/// <param name="repository">The repository name that holds our snapshot</param>
		/// <param name="snapshotName">The name of the snapshot that we want to restore</param>
		/// <param name="selector">Optionally further describe the restore operation</param>
		RestoreResponse Restore(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc />
		RestoreResponse Restore(IRestoreRequest request);

		/// <inheritdoc />
		Task<RestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<RestoreResponse> RestoreAsync(IRestoreRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RestoreResponse Restore(IRestoreRequest request) =>
			DoRequest<IRestoreRequest, RestoreResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public RestoreResponse Restore(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null) =>
			Restore(selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public Task<RestoreResponse> RestoreAsync(IRestoreRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRestoreRequest, RestoreResponse, RestoreResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public Task<RestoreResponse> RestoreAsync(
			Name repository,
			Name snapshotName,
			Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken ct = default
		) => RestoreAsync(selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshotName)), ct);
	}
}
