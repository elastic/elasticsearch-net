using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc />
		IGetRoleResponse GetRole(IGetRoleRequest request);

		/// <inheritdoc />
		Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null) =>
			GetRole(selector.InvokeOrDefault(new GetRoleDescriptor()));

		/// <inheritdoc />
		public IGetRoleResponse GetRole(IGetRoleRequest request) =>
			Dispatcher.Dispatch<IGetRoleRequest, GetRoleRequestParameters, GetRoleResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityGetRoleDispatch<GetRoleResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetRoleAsync(selector.InvokeOrDefault(new GetRoleDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetRoleRequest, GetRoleRequestParameters, GetRoleResponse, IGetRoleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SecurityGetRoleDispatchAsync<GetRoleResponse>(p, c)
			);
	}
}
