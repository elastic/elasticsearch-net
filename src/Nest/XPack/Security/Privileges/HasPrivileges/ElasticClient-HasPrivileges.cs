using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Determine whether the authenticated user has a specified list of privileges.
		/// </summary>
		HasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		HasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		Task<HasPrivilegesResponse> HasPrivilegesAsync(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		Task<HasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public HasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null) =>
			HasPrivileges(selector.InvokeOrDefault(new HasPrivilegesDescriptor()));

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public HasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request) =>
			DoRequest<IHasPrivilegesRequest, HasPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public Task<HasPrivilegesResponse> HasPrivilegesAsync(
			Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken ct = default
		) => HasPrivilegesAsync(selector.InvokeOrDefault(new HasPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public Task<HasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IHasPrivilegesRequest, HasPrivilegesResponse, HasPrivilegesResponse>
				(request, request.RequestParameters, ct);
	}
}
