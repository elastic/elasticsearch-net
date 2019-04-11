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
		IIlmRetryResponse IlmRetry(IndexName index, Func<IlmRetryDescriptor, IIlmRetryRequest> selector = null);

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		IIlmRetryResponse IlmRetry(IIlmRetryRequest request);

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		Task<IIlmRetryResponse> IlmRetryAsync(IndexName index, Func<IlmRetryDescriptor, IIlmRetryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		Task<IIlmRetryResponse> IlmRetryAsync(IIlmRetryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		public IIlmRetryResponse IlmRetry(IndexName index, Func<IlmRetryDescriptor, IIlmRetryRequest> selector = null) =>
			IlmRetry(selector.InvokeOrDefault(new IlmRetryDescriptor(index)));

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		public IIlmRetryResponse IlmRetry(IIlmRetryRequest request) =>
			Dispatcher.Dispatch<IIlmRetryRequest, IlmRetryRequestParameters, IlmRetryResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmRetryDispatch<IlmRetryResponse>(p)
			);

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		public Task<IIlmRetryResponse> IlmRetryAsync(IndexName index, Func<IlmRetryDescriptor, IIlmRetryRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmRetryAsync(selector.InvokeOrDefault(new IlmRetryDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="IlmRetry(Nest.IndexName,System.Func{Nest.IlmRetryDescriptor,Nest.IIlmRetryRequest})" />
		public Task<IIlmRetryResponse> IlmRetryAsync(IIlmRetryRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmRetryRequest, IlmRetryRequestParameters, IlmRetryResponse, IIlmRetryResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmRetryDispatchAsync<IlmRetryResponse>(p, c)
			);
	}
}
