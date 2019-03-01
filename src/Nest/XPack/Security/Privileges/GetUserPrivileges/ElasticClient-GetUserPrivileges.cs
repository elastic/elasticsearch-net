using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves application privileges for authenticated user.
		/// </summary>
		IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null) =>
			GetUserPrivileges(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()));
		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request) =>
			Dispatcher.Dispatch<IGetUserPrivilegesRequest, GetUserPrivilegesRequestParameters, GetUserPrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityGetUserPrivilegesDispatch<GetUserPrivilegesResponse>(p)
			);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => GetUserPrivilegesAsync(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetUserPrivilegesRequest, GetUserPrivilegesRequestParameters, GetUserPrivilegesResponse, IGetUserPrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityGetUserPrivilegesDispatchAsync<GetUserPrivilegesResponse>(p, c)
			);
	}
}
