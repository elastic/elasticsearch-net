using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null);

		/// <inheritdoc />
		IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request);

		/// <inheritdoc />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null) =>
			GetPrivileges(selector.InvokeOrDefault(new GetPrivilegesDescriptor()));

		/// <inheritdoc />
		public IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request) =>
			Dispatcher.Dispatch<IGetPrivilegesRequest, GetPrivilegesRequestParameters, GetPrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityGetPrivilegesDispatch<GetPrivilegesResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetPrivilegesAsync(selector.InvokeOrDefault(new GetPrivilegesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetPrivilegesRequest, GetPrivilegesRequestParameters, GetPrivilegesResponse, IGetPrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityGetPrivilegesDispatchAsync<GetPrivilegesResponse>(p, c)
			);
	}
}
