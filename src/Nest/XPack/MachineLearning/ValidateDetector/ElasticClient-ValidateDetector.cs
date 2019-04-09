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
		IValidateDetectorResponse ValidateDetector<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector) where T : class;

		/// <inheritdoc />
		IValidateDetectorResponse ValidateDetector(IValidateDetectorRequest request);

		/// <inheritdoc />
		Task<IValidateDetectorResponse> ValidateDetectorAsync<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<IValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IValidateDetectorResponse ValidateDetector<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector)
			where T : class =>
			ValidateDetector(selector.InvokeOrDefault(new ValidateDetectorDescriptor<T>()));

		/// <inheritdoc />
		public IValidateDetectorResponse ValidateDetector(IValidateDetectorRequest request) =>
			Dispatch2<IValidateDetectorRequest, ValidateDetectorResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IValidateDetectorResponse> ValidateDetectorAsync<T>(
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateDetectorAsync(selector.InvokeOrDefault(new ValidateDetectorDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<IValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IValidateDetectorRequest, IValidateDetectorResponse, ValidateDetectorResponse>
				(request, request.RequestParameters, ct);
	}
}
