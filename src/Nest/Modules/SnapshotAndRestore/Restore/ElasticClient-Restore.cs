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
		IRestoreResponse Restore(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc />
		IRestoreResponse Restore(IRestoreRequest request);

		/// <inheritdoc />
		Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IRestoreResponse> RestoreAsync(IRestoreRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRestoreResponse Restore(IRestoreRequest request) =>
			Dispatcher.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				request,
				LowLevelDispatch.SnapshotRestoreDispatch<RestoreResponse>
			);

		/// <inheritdoc />
		public IRestoreResponse Restore(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null) =>
			Restore(selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshotName)));

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(IRestoreRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d, c)
			);

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RestoreAsync(selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshotName)), cancellationToken);
	}
}
