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
		IValidateJobResponse ValidateJob<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector) where T : class;

		/// <inheritdoc />
		IValidateJobResponse ValidateJob(IValidateJobRequest request);

		/// <inheritdoc />
		Task<IValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IValidateJobResponse ValidateJob<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector)
			where T : class =>
			ValidateJob(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()));

		/// <inheritdoc />
		public IValidateJobResponse ValidateJob(IValidateJobRequest request) =>
			DoRequest<IValidateJobRequest, ValidateJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateJobAsync(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IValidateJobRequest, IValidateJobResponse, ValidateJobResponse>
				(request, request.RequestParameters, ct);
	}
}
