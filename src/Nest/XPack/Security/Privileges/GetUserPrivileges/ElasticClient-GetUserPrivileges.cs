using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null);

		/// <inheritdoc />
		IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request);

		/// <inheritdoc />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null) =>
			GetUserPrivileges(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()));

		/// <inheritdoc />
		public IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request) =>
			Dispatcher.Dispatch<IGetUserPrivilegesRequest, GetUserPrivilegesRequestParameters, GetUserPrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityGetUserPrivilegesDispatch<GetUserPrivilegesResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetUserPrivilegesAsync(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetUserPrivilegesRequest, GetUserPrivilegesRequestParameters, GetUserPrivilegesResponse, IGetUserPrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityGetUserPrivilegesDispatchAsync<GetUserPrivilegesResponse>(p, c)
			);
	}
}
