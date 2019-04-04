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
		IHasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		IHasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		Task<IHasPrivilegesResponse> HasPrivilegesAsync(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		Task<IHasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public IHasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null) =>
			HasPrivileges(selector.InvokeOrDefault(new HasPrivilegesDescriptor()));

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public IHasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request) =>
			Dispatcher.Dispatch<IHasPrivilegesRequest, HasPrivilegesRequestParameters, HasPrivilegesResponse>(
				request,
				LowLevelDispatch.XpackSecurityHasPrivilegesDispatch<HasPrivilegesResponse>
			);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public Task<IHasPrivilegesResponse> HasPrivilegesAsync(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			HasPrivilegesAsync(selector.InvokeOrDefault(new HasPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public Task<IHasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IHasPrivilegesRequest, HasPrivilegesRequestParameters, HasPrivilegesResponse, IHasPrivilegesResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackSecurityHasPrivilegesDispatchAsync<HasPrivilegesResponse>
			);
	}
}
