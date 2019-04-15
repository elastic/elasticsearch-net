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
		DeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null);

		/// <inheritdoc />
		DeleteJobResponse DeleteJob(IDeleteJobRequest request);

		/// <inheritdoc />
		Task<DeleteJobResponse> DeleteJobAsync(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null) =>
			DeleteJob(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)));

		/// <inheritdoc />
		public DeleteJobResponse DeleteJob(IDeleteJobRequest request) =>
			DoRequest<IDeleteJobRequest, DeleteJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteJobResponse> DeleteJobAsync(
			Id jobId,
			Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		) => DeleteJobAsync(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<DeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteJobRequest, DeleteJobResponse, DeleteJobResponse>(request, request.RequestParameters, ct);
	}
}
