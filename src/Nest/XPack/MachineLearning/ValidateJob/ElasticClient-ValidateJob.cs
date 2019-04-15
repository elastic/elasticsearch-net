using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Validates a machine learning job
		/// </summary>
		ValidateJobResponse ValidateJob<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector) where T : class;

		/// <inheritdoc />
		ValidateJobResponse ValidateJob(IValidateJobRequest request);

		/// <inheritdoc />
		Task<ValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<ValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ValidateJobResponse ValidateJob<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector)
			where T : class =>
			ValidateJob(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()));

		/// <inheritdoc />
		public ValidateJobResponse ValidateJob(IValidateJobRequest request) =>
			DoRequest<IValidateJobRequest, ValidateJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateJobAsync(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<ValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IValidateJobRequest, ValidateJobResponse>
				(request, request.RequestParameters, ct);
	}
}
