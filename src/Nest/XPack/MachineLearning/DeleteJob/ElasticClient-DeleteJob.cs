using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a machine learning job.
		/// Before you can delete a job, you must delete the datafeeds that are associated with it, see DeleteDatafeed.
		/// Unless the force parameter is used, the job must be closed before it can be deleted.
		/// </summary>
		/// <remarks>
		/// It is not currently possible to delete multiple jobs, either using wildcards or a comma separated list.
		/// </remarks>
		IDeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null);

		/// <inheritdoc />
		IDeleteJobResponse DeleteJob(IDeleteJobRequest request);

		/// <inheritdoc />
		Task<IDeleteJobResponse> DeleteJobAsync(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null) =>
			DeleteJob(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)));

		/// <inheritdoc />
		public IDeleteJobResponse DeleteJob(IDeleteJobRequest request) =>
			Dispatch2<IDeleteJobRequest, DeleteJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteJobResponse> DeleteJobAsync(
			Id jobId,
			Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		) => DeleteJobAsync(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteJobRequest, IDeleteJobResponse, DeleteJobResponse>(request, request.RequestParameters, ct);
	}
}
