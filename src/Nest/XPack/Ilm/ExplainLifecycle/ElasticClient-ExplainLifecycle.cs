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
		ExplainLifecycleResponse ExplainLifecycle(IndexName index, Func<ExplainLifecycleDescriptor, IExplainLifecycleRequest> selector = null);

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		ExplainLifecycleResponse ExplainLifecycle(IExplainLifecycleRequest request);

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		Task<ExplainLifecycleResponse> ExplainLifecycleAsync(IndexName index, Func<ExplainLifecycleDescriptor, IExplainLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		Task<ExplainLifecycleResponse> ExplainLifecycleAsync(IExplainLifecycleRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		public ExplainLifecycleResponse ExplainLifecycle(IndexName index, Func<ExplainLifecycleDescriptor, IExplainLifecycleRequest> selector = null) =>
			ExplainLifecycle(selector.InvokeOrDefault(new ExplainLifecycleDescriptor(index)));

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		public ExplainLifecycleResponse ExplainLifecycle(IExplainLifecycleRequest request) =>
			DoRequest<IExplainLifecycleRequest, ExplainLifecycleResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		public Task<ExplainLifecycleResponse> ExplainLifecycleAsync(IndexName index, Func<ExplainLifecycleDescriptor, IExplainLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			ExplainLifecycleAsync(selector.InvokeOrDefault(new ExplainLifecycleDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="ExplainLifecycle(Nest.IndexName,System.Func{Nest.ExplainLifecycleDescriptor,Nest.IExplainLifecycleRequest})" />
		public Task<ExplainLifecycleResponse> ExplainLifecycleAsync(IExplainLifecycleRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IExplainLifecycleRequest, ExplainLifecycleResponse>(request, request.RequestParameters, cancellationToken);
	}
}
