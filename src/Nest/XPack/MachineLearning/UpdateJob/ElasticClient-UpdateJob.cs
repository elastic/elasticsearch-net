using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Updates a Machine Learning job.
		/// </summary>
		IUpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IUpdateJobResponse UpdateJob(IUpdateJobRequest request);

		/// <inheritdoc/>
		Task<IUpdateJobResponse> UpdateJobAsync<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class =>
			this.UpdateJob(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)));

		/// <inheritdoc/>
		public IUpdateJobResponse UpdateJob(IUpdateJobRequest request) =>
			this.Dispatcher.Dispatch<IUpdateJobRequest, UpdateJobRequestParameters, UpdateJobResponse>(
				request,
				this.LowLevelDispatch.XpackMlUpdateJobDispatch<UpdateJobResponse>
			);

		/// <inheritdoc/>
		public Task<IUpdateJobResponse> UpdateJobAsync<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.UpdateJobAsync(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IUpdateJobRequest, UpdateJobRequestParameters, UpdateJobResponse, IUpdateJobResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlUpdateJobDispatchAsync<UpdateJobResponse>
			);
	}
}
