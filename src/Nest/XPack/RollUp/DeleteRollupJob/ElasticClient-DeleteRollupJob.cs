using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an existing rollup job. The job can be started or stopped, in both cases it will be deleted.
		/// Attempting to delete a non-existing job will throw an exception
		/// </summary>
		DeleteRollupJobResponse DeleteRollupJob(Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		DeleteRollupJobResponse DeleteRollupJob(IDeleteRollupJobRequest request);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		Task<DeleteRollupJobResponse> DeleteRollupJobAsync(Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		Task<DeleteRollupJobResponse> DeleteRollupJobAsync(IDeleteRollupJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteRollupJobResponse DeleteRollupJob(Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null) =>
			DeleteRollupJob(selector.InvokeOrDefault(new DeleteRollupJobDescriptor(id)));

		/// <inheritdoc />
		public DeleteRollupJobResponse DeleteRollupJob(IDeleteRollupJobRequest request) =>
			DoRequest<IDeleteRollupJobRequest, DeleteRollupJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteRollupJobResponse> DeleteRollupJobAsync(
			Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRollupJobAsync(selector.InvokeOrDefault(new DeleteRollupJobDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<DeleteRollupJobResponse> DeleteRollupJobAsync(IDeleteRollupJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRollupJobRequest, DeleteRollupJobResponse>(request, request.RequestParameters, ct);
	}
}
