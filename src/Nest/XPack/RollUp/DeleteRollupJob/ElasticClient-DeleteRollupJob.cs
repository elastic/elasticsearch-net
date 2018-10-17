using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an existing rollup job. The job can be started or stopped, in both cases it will be deleted.
		/// Attempting to delete a non-existing job will throw an exception
		/// </summary>
		IDeleteRollupJobResponse DeleteRollupJob(Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		IDeleteRollupJobResponse DeleteRollupJob(IDeleteRollupJobRequest request);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		Task<IDeleteRollupJobResponse> DeleteRollupJobAsync(Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		Task<IDeleteRollupJobResponse> DeleteRollupJobAsync(IDeleteRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteRollupJobResponse DeleteRollupJob(Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null) =>
			this.DeleteRollupJob(selector.InvokeOrDefault(new DeleteRollupJobDescriptor(id)));

		/// <inheritdoc/>
		public IDeleteRollupJobResponse DeleteRollupJob(IDeleteRollupJobRequest request) =>
			this.Dispatcher.Dispatch<IDeleteRollupJobRequest, DeleteRollupJobRequestParameters, DeleteRollupJobResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupDeleteJobDispatch<DeleteRollupJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteRollupJobResponse> DeleteRollupJobAsync(
			Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		)  =>
			this.DeleteRollupJobAsync(selector.InvokeOrDefault(new DeleteRollupJobDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteRollupJobResponse> DeleteRollupJobAsync(IDeleteRollupJobRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IDeleteRollupJobRequest, DeleteRollupJobRequestParameters, DeleteRollupJobResponse, IDeleteRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupDeleteJobDispatchAsync<DeleteRollupJobResponse>(p, c)
			);
	}
}
