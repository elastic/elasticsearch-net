using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector);

		/// <inheritdoc />
		IPutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request);

		/// <inheritdoc />
		Task<IPutPrivilegesResponse> PutPrivilegesAsync(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken cancellationToken = default);

		/// <inheritdoc />
		Task<IPutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector) =>
			PutPrivileges(selector.InvokeOrDefault(new PutPrivilegesDescriptor()));

		/// <inheritdoc />
		public IPutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request) =>
			Dispatcher.Dispatch<IPutPrivilegesRequest, PutPrivilegesRequestParameters, PutPrivilegesResponse>(
				request,
				LowLevelDispatch.XpackSecurityPutPrivilegesDispatch<PutPrivilegesResponse>
			);

		/// <inheritdoc />
		public Task<IPutPrivilegesResponse> PutPrivilegesAsync(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector,
			CancellationToken cancellationToken = default
		) =>
			PutPrivilegesAsync(selector.InvokeOrDefault(new PutPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IPutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IPutPrivilegesRequest, PutPrivilegesRequestParameters, PutPrivilegesResponse, IPutPrivilegesResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackSecurityPutPrivilegesDispatchAsync<PutPrivilegesResponse>
			);
	}
}
