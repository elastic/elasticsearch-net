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
		UpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class;

		/// <inheritdoc />
		UpdateJobResponse UpdateJob(IUpdateJobRequest request);

		/// <inheritdoc />
		Task<UpdateJobResponse> UpdateJobAsync<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<UpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateJobResponse UpdateJob<T>(Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null)
			where T : class =>
			UpdateJob(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)));

		/// <inheritdoc />
		public UpdateJobResponse UpdateJob(IUpdateJobRequest request) =>
			DoRequest<IUpdateJobRequest, UpdateJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateJobResponse> UpdateJobAsync<T>(
			Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			UpdateJobAsync(selector.InvokeOrDefault(new UpdateJobDescriptor<T>(jobId)), ct);

		/// <inheritdoc />
		public Task<UpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateJobRequest, UpdateJobResponse>
				(request, request.RequestParameters, ct);
	}
}
