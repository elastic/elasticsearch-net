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

		/// <inheritdoc />
		IPutJobResponse PutJob(IPutJobRequest request);

		/// <inheritdoc />
		Task<IPutJobResponse> PutJobAsync<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutJobResponse PutJob<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector) where T : class =>
			PutJob(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)));

		/// <inheritdoc />
		public IPutJobResponse PutJob(IPutJobRequest request) =>
			Dispatcher.Dispatch<IPutJobRequest, PutJobRequestParameters, PutJobResponse>(
				request,
				LowLevelDispatch.MlPutJobDispatch<PutJobResponse>
			);

		/// <inheritdoc />
		public Task<IPutJobResponse> PutJobAsync<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			PutJobAsync(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IPutJobRequest, PutJobRequestParameters, PutJobResponse, IPutJobResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlPutJobDispatchAsync<PutJobResponse>
			);
	}
}
