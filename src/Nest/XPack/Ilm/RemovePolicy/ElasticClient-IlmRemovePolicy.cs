using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Removes the assigned lifecycle policy from an index.
		/// Removes the assigned lifecycle policy and stops managing the specified index.
		/// If an index pattern is specified, removes the assigned policies from all matching indices.
		/// </summary>
		IIlmRemovePolicyResponse IlmRemovePolicy(Func<IlmRemovePolicyDescriptor, IIlmRemovePolicyRequest> selector = null);

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		IIlmRemovePolicyResponse IlmRemovePolicy(IIlmRemovePolicyRequest request);

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		Task<IIlmRemovePolicyResponse> IlmRemovePolicyAsync(Func<IlmRemovePolicyDescriptor, IIlmRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		Task<IIlmRemovePolicyResponse> IlmRemovePolicyAsync(IIlmRemovePolicyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		public IIlmRemovePolicyResponse IlmRemovePolicy(Func<IlmRemovePolicyDescriptor, IIlmRemovePolicyRequest> selector = null) =>
			IlmRemovePolicy(selector.InvokeOrDefault(new IlmRemovePolicyDescriptor()));

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		public IIlmRemovePolicyResponse IlmRemovePolicy(IIlmRemovePolicyRequest request) =>
			Dispatcher.Dispatch<IIlmRemovePolicyRequest, IlmRemovePolicyRequestParameters, IlmRemovePolicyResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmRemovePolicyDispatch<IlmRemovePolicyResponse>(p)
			);

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		public Task<IIlmRemovePolicyResponse> IlmRemovePolicyAsync(Func<IlmRemovePolicyDescriptor, IIlmRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmRemovePolicyAsync(selector.InvokeOrDefault(new IlmRemovePolicyDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmRemovePolicy(System.Func{Nest.IlmRemovePolicyDescriptor,Nest.IIlmRemovePolicyRequest})" />
		public Task<IIlmRemovePolicyResponse> IlmRemovePolicyAsync(IIlmRemovePolicyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmRemovePolicyRequest, IlmRemovePolicyRequestParameters, IlmRemovePolicyResponse, IIlmRemovePolicyResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmRemovePolicyDispatchAsync<IlmRemovePolicyResponse>(p, c)
			);
	}
}
