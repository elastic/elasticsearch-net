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
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutJobResponse PutJob<T>(Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector)
			where T : class =>
			PutJob(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)));

		/// <inheritdoc />
		public IPutJobResponse PutJob(IPutJobRequest request) =>
			DoRequest<IPutJobRequest, PutJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutJobResponse> PutJobAsync<T>(
			Id jobId,
			Func<PutJobDescriptor<T>, IPutJobRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			PutJobAsync(selector.InvokeOrDefault(new PutJobDescriptor<T>(jobId)), ct);

		/// <inheritdoc />
		public Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutJobRequest, IPutJobResponse, PutJobResponse>(request, request.RequestParameters, ct);
	}
}
