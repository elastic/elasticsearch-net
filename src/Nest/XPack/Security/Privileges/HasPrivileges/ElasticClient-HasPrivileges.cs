using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IHasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null);

		/// <inheritdoc />
		IHasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request);

		/// <inheritdoc />
		Task<IHasPrivilegesResponse> HasPrivilegesAsync(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IHasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IHasPrivilegesResponse HasPrivileges(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null) =>
			HasPrivileges(selector.InvokeOrDefault(new HasPrivilegesDescriptor()));

		/// <inheritdoc />
		public IHasPrivilegesResponse HasPrivileges(IHasPrivilegesRequest request) =>
			Dispatcher.Dispatch<IHasPrivilegesRequest, HasPrivilegesRequestParameters, HasPrivilegesResponse>(
				request,
				LowLevelDispatch.XpackSecurityHasPrivilegesDispatch<HasPrivilegesResponse>
			);

		/// <inheritdoc />
		public Task<IHasPrivilegesResponse> HasPrivilegesAsync(Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			HasPrivilegesAsync(selector.InvokeOrDefault(new HasPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IHasPrivilegesResponse> HasPrivilegesAsync(IHasPrivilegesRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IHasPrivilegesRequest, HasPrivilegesRequestParameters, HasPrivilegesResponse, IHasPrivilegesResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackSecurityHasPrivilegesDispatchAsync<HasPrivilegesResponse>
			);
	}
}
