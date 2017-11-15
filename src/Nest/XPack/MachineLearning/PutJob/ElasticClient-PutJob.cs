using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a machine learning job.
		/// </summary>
		IPutJobResponse PutJob<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector) where T : class;

		/// <inheritdoc/>
		IPutJobResponse PutJob(IPutJobRequest request);

		/// <inheritdoc/>
		Task<IPutJobResponse> PutJobAsync<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutJobResponse PutJob<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector) where T : class =>
			this.PutJob(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)));

		/// <inheritdoc/>
		public IPutJobResponse PutJob(IPutJobRequest request) =>
			this.Dispatcher.Dispatch<IPutJobRequest, PutJobRequestParameters, PutJobResponse>(
				request,
				this.LowLevelDispatch.XpackMlPutJobDispatch<PutJobResponse>
			);

		/// <inheritdoc/>
		public Task<IPutJobResponse> PutJobAsync<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.PutJobAsync(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutJobRequest, PutJobRequestParameters, PutJobResponse, IPutJobResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPutJobDispatchAsync<PutJobResponse>
			);
	}
}
