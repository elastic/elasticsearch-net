using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Updates a machine learning job.
		/// </summary>
		IUpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class;

		/// <inheritdoc />
		IUpdateJobResponse UpdateJob(IUpdateJobRequest request);

		/// <inheritdoc />
		Task<IUpdateJobResponse> UpdateJobAsync<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class =>
			UpdateJob(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)));

		/// <inheritdoc />
		public IUpdateJobResponse UpdateJob(IUpdateJobRequest request) =>
			Dispatcher.Dispatch<IUpdateJobRequest, UpdateJobRequestParameters, UpdateJobResponse>(
				request,
				LowLevelDispatch.MlUpdateJobDispatch<UpdateJobResponse>
			);

		/// <inheritdoc />
		public Task<IUpdateJobResponse> UpdateJobAsync<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			UpdateJobAsync(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IUpdateJobRequest, UpdateJobRequestParameters, UpdateJobResponse, IUpdateJobResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlUpdateJobDispatchAsync<UpdateJobResponse>
			);
	}
}
