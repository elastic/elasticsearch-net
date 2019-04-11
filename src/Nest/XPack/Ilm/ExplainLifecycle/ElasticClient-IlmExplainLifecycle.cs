using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Shows an index’s current lifecycle status.
		/// Retrieves information about the index’s current lifecycle state, such as the currently executing phase, action, and step.
		/// Shows when the index entered each one, the definition of the running phase, and information about any failures.
		/// </summary>
		IIlmExplainLifecycleResponse IlmExplainLifecycle(IndexName index, Func<IlmExplainLifecycleDescriptor, IIlmExplainLifecycleRequest> selector = null);

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		IIlmExplainLifecycleResponse IlmExplainLifecycle(IIlmExplainLifecycleRequest request);

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		Task<IIlmExplainLifecycleResponse> IlmExplainLifecycleAsync(IndexName index, Func<IlmExplainLifecycleDescriptor, IIlmExplainLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		Task<IIlmExplainLifecycleResponse> IlmExplainLifecycleAsync(IIlmExplainLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		public IIlmExplainLifecycleResponse IlmExplainLifecycle(IndexName index, Func<IlmExplainLifecycleDescriptor, IIlmExplainLifecycleRequest> selector = null) =>
			IlmExplainLifecycle(selector.InvokeOrDefault(new IlmExplainLifecycleDescriptor(index)));

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		public IIlmExplainLifecycleResponse IlmExplainLifecycle(IIlmExplainLifecycleRequest request) =>
			Dispatcher.Dispatch<IIlmExplainLifecycleRequest, IlmExplainLifecycleRequestParameters, IlmExplainLifecycleResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmExplainLifecycleDispatch<IlmExplainLifecycleResponse>(p)
			);

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		public Task<IIlmExplainLifecycleResponse> IlmExplainLifecycleAsync(IndexName index, Func<IlmExplainLifecycleDescriptor, IIlmExplainLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmExplainLifecycleAsync(selector.InvokeOrDefault(new IlmExplainLifecycleDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="IlmExplainLifecycle(Nest.IndexName,System.Func{Nest.IlmExplainLifecycleDescriptor,Nest.IIlmExplainLifecycleRequest})" />
		public Task<IIlmExplainLifecycleResponse> IlmExplainLifecycleAsync(IIlmExplainLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmExplainLifecycleRequest, IlmExplainLifecycleRequestParameters, IlmExplainLifecycleResponse, IIlmExplainLifecycleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmExplainLifecycleDispatchAsync<IlmExplainLifecycleResponse>(p, c)
			);
	}
}
