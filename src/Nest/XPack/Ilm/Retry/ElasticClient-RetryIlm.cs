using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retry executing the policy for an index that is in the ERROR step.
		/// Sets the policy back to the step where the error occurred and executes the step.
		/// Use the Explain API to determine if an index is in the ERROR step.
		/// </summary>
		RetryIlmResponse RetryIlm(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		RetryIlmResponse RetryIlm(IRetryIlmRequest request);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		Task<RetryIlmResponse> RetryIlmAsync(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		Task<RetryIlmResponse> RetryIlmAsync(IRetryIlmRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public RetryIlmResponse RetryIlm(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null) =>
			RetryIlm(selector.InvokeOrDefault(new RetryIlmDescriptor(index)));

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public RetryIlmResponse RetryIlm(IRetryIlmRequest request) =>
			DoRequest<IRetryIlmRequest, RetryIlmResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public Task<RetryIlmResponse> RetryIlmAsync(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			RetryIlmAsync(selector.InvokeOrDefault(new RetryIlmDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public Task<RetryIlmResponse> RetryIlmAsync(IRetryIlmRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IRetryIlmRequest, RetryIlmResponse>(request, request.RequestParameters, cancellationToken);
	}
}
