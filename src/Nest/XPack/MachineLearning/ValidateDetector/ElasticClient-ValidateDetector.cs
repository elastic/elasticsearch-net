using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Validates a detector for a machine learning job
		/// </summary>
		ValidateDetectorResponse ValidateDetector<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector) where T : class;

		/// <inheritdoc />
		ValidateDetectorResponse ValidateDetector(IValidateDetectorRequest request);

		/// <inheritdoc />
		Task<ValidateDetectorResponse> ValidateDetectorAsync<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<ValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ValidateDetectorResponse ValidateDetector<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector)
			where T : class =>
			ValidateDetector(selector.InvokeOrDefault(new ValidateDetectorDescriptor<T>()));

		/// <inheritdoc />
		public ValidateDetectorResponse ValidateDetector(IValidateDetectorRequest request) =>
			DoRequest<IValidateDetectorRequest, ValidateDetectorResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ValidateDetectorResponse> ValidateDetectorAsync<T>(
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateDetectorAsync(selector.InvokeOrDefault(new ValidateDetectorDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<ValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IValidateDetectorRequest, ValidateDetectorResponse>
				(request, request.RequestParameters, ct);
	}
}
