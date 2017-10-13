using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a Machine Learning job.
		/// Before you can delete a job, you must delete the datafeeds that are associated with it, see DeleteDatafeed. Unless the force parameter is used the job must be closed before it can be deleted.
		/// It is not currently possible to delete multiple jobs using wildcards or a comma separated list.
		/// </summary>
		IDeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null);

		/// <inheritdoc/>
		IDeleteJobResponse DeleteJob(IDeleteJobRequest request);

		/// <inheritdoc/>
		Task<IDeleteJobResponse> DeleteJobAsync(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null) =>
			this.DeleteJob(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)));

		/// <inheritdoc/>
		public IDeleteJobResponse DeleteJob(IDeleteJobRequest request) =>
			this.Dispatcher.Dispatch<IDeleteJobRequest, DeleteJobRequestParameters, DeleteJobResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlDeleteJobDispatch<DeleteJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteJobResponse> DeleteJobAsync(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteJobAsync(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteJobRequest, DeleteJobRequestParameters, DeleteJobResponse, IDeleteJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlDeleteJobDispatchAsync<DeleteJobResponse>(p, c)
			);
	}
}
