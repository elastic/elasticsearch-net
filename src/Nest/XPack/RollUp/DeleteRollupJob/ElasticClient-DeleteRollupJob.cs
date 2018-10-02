using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteRollupJobResponse DeleteRollupJob(Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null);

		/// <inheritdoc/>
		IDeleteRollupJobResponse DeleteRollupJob(IDeleteRollupJobRequest request);

		/// <inheritdoc/>
		Task<IDeleteRollupJobResponse> DeleteRollupJobAsync(Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
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
