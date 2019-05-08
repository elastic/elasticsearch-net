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
		IRetryIlmResponse RetryIlm(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		IRetryIlmResponse RetryIlm(IRetryIlmRequest request);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		Task<IRetryIlmResponse> RetryIlmAsync(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		Task<IRetryIlmResponse> RetryIlmAsync(IRetryIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public IRetryIlmResponse RetryIlm(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null) =>
			RetryIlm(selector.InvokeOrDefault(new RetryIlmDescriptor(index)));

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public IRetryIlmResponse RetryIlm(IRetryIlmRequest request) =>
			Dispatcher.Dispatch<IRetryIlmRequest, RetryIlmRequestParameters, RetryIlmResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmRetryDispatch<RetryIlmResponse>(p)
			);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public Task<IRetryIlmResponse> RetryIlmAsync(IndexName index, Func<RetryIlmDescriptor, IRetryIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RetryIlmAsync(selector.InvokeOrDefault(new RetryIlmDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="RetryIlm(Nest.IndexName,System.Func{Nest.RetryIlmDescriptor,Nest.IRetryIlmRequest})" />
		public Task<IRetryIlmResponse> RetryIlmAsync(IRetryIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRetryIlmRequest, RetryIlmRequestParameters, RetryIlmResponse, IRetryIlmResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmRetryDispatchAsync<RetryIlmResponse>(p, c)
			);
	}
}
