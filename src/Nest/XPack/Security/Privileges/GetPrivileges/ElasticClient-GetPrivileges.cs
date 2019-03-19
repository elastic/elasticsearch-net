using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves application privileges.
		/// </summary>
		IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null) =>
			GetPrivileges(selector.InvokeOrDefault(new GetPrivilegesDescriptor()));

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request) =>
			Dispatcher.Dispatch<IGetPrivilegesRequest, GetPrivilegesRequestParameters, GetPrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityGetPrivilegesDispatch<GetPrivilegesResponse>(p)
			);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetPrivilegesAsync(selector.InvokeOrDefault(new GetPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetPrivilegesRequest, GetPrivilegesRequestParameters, GetPrivilegesResponse, IGetPrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityGetPrivilegesDispatchAsync<GetPrivilegesResponse>(p, c)
			);
	}
}
