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
		IRemovePolicyResponse RemovePolicy(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		IRemovePolicyResponse RemovePolicy(IRemovePolicyRequest request);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		Task<IRemovePolicyResponse> RemovePolicyAsync(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		Task<IRemovePolicyResponse> RemovePolicyAsync(IRemovePolicyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public IRemovePolicyResponse RemovePolicy(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null) =>
			RemovePolicy(selector.InvokeOrDefault(new RemovePolicyDescriptor(index)));

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public IRemovePolicyResponse RemovePolicy(IRemovePolicyRequest request) =>
			Dispatcher.Dispatch<IRemovePolicyRequest, RemovePolicyRequestParameters, RemovePolicyResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmRemovePolicyDispatch<RemovePolicyResponse>(p)
			);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public Task<IRemovePolicyResponse> RemovePolicyAsync(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RemovePolicyAsync(selector.InvokeOrDefault(new RemovePolicyDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public Task<IRemovePolicyResponse> RemovePolicyAsync(IRemovePolicyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRemovePolicyRequest, RemovePolicyRequestParameters, RemovePolicyResponse, IRemovePolicyResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmRemovePolicyDispatchAsync<RemovePolicyResponse>(p, c)
			);
	}
}
