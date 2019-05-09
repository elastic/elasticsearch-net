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
		RemovePolicyResponse RemovePolicy(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		RemovePolicyResponse RemovePolicy(IRemovePolicyRequest request);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		Task<RemovePolicyResponse> RemovePolicyAsync(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		Task<RemovePolicyResponse> RemovePolicyAsync(IRemovePolicyRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public RemovePolicyResponse RemovePolicy(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null) =>
			RemovePolicy(selector.InvokeOrDefault(new RemovePolicyDescriptor(index)));

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public RemovePolicyResponse RemovePolicy(IRemovePolicyRequest request) =>
			DoRequest<IRemovePolicyRequest, RemovePolicyResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public Task<RemovePolicyResponse> RemovePolicyAsync(IndexName index, Func<RemovePolicyDescriptor, IRemovePolicyRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			RemovePolicyAsync(selector.InvokeOrDefault(new RemovePolicyDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="RemovePolicy(Nest.IndexName,System.Func{Nest.RemovePolicyDescriptor,Nest.IRemovePolicyRequest})" />
		public Task<RemovePolicyResponse> RemovePolicyAsync(IRemovePolicyRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IRemovePolicyRequest, RemovePolicyResponse>(request, request.RequestParameters, cancellationToken);
	}
}
