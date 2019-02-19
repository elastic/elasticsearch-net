using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc />
		IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request);

		/// <inheritdoc />
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null) =>
			ClearCachedRoles(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)));

		/// <inheritdoc />
		public IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request) =>
			Dispatcher.Dispatch<IClearCachedRolesRequest, ClearCachedRolesRequestParameters, ClearCachedRolesResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityClearCachedRolesDispatch<ClearCachedRolesResponse>(p)
			);

		/// <inheritdoc />
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles,
			Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClearCachedRolesAsync(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)), cancellationToken);

		/// <inheritdoc />
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IClearCachedRolesRequest, ClearCachedRolesRequestParameters, ClearCachedRolesResponse, IClearCachedRolesResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityClearCachedRolesDispatchAsync<ClearCachedRolesResponse>(p, c)
				);
	}
}
