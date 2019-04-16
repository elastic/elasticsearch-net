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
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null)
			where T : class =>
			UpdateJob(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)));

		/// <inheritdoc />
		public IUpdateJobResponse UpdateJob(IUpdateJobRequest request) =>
			DoRequest<IUpdateJobRequest, UpdateJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IUpdateJobResponse> UpdateJobAsync<T>(
			Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			UpdateJobAsync(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)), ct);

		/// <inheritdoc />
		public Task<IUpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateJobRequest, IUpdateJobResponse, UpdateJobResponse>
				(request, request.RequestParameters, ct);
	}
}
