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
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<IValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
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
			Dispatcher.Dispatch<IValidateDetectorRequest, ValidateDetectorRequestParameters, ValidateDetectorResponse>(
				request,
				LowLevelDispatch.MlValidateDetectorDispatch<ValidateDetectorResponse>
			);

		/// <inheritdoc />
		public Task<IValidateDetectorResponse> ValidateDetectorAsync<T>(Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			ValidateDetectorAsync(selector.InvokeOrDefault(new ValidateDetectorDescriptor<T>()), cancellationToken);

		/// <inheritdoc />
		public Task<IValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IValidateDetectorRequest, ValidateDetectorRequestParameters, ValidateDetectorResponse, IValidateDetectorResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.MlValidateDetectorDispatchAsync<ValidateDetectorResponse>
				);
	}
}
