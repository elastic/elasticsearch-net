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

		/// <inheritdoc/>
		IValidateJobResponse ValidateJob(IValidateJobRequest request);

		/// <inheritdoc/>
		Task<IValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IValidateJobResponse ValidateJob<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector) where T : class =>
			this.ValidateJob(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()));

		/// <inheritdoc/>
		public IValidateJobResponse ValidateJob(IValidateJobRequest request) =>
			this.Dispatcher.Dispatch<IValidateJobRequest, ValidateJobRequestParameters, ValidateJobResponse>(
				request,
				this.LowLevelDispatch.XpackMlValidateDispatch<ValidateJobResponse>
			);

		/// <inheritdoc/>
		public Task<IValidateJobResponse> ValidateJobAsync<T>(Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector, CancellationToken cancellationToken = default(CancellationToken))  where T : class =>
			this.ValidateJobAsync(selector.InvokeOrDefault(new ValidateJobDescriptor<T>()), cancellationToken);

		/// <inheritdoc/>
		public Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IValidateJobRequest, ValidateJobRequestParameters, ValidateJobResponse, IValidateJobResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlValidateDispatchAsync<ValidateJobResponse>
			);
	}
}
