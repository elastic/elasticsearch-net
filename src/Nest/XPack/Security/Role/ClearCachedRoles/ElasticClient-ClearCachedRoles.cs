using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc/>
		IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request);

		/// <inheritdoc/>
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null) =>
			this.ClearCachedRoles(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)));

		/// <inheritdoc/>
		public IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request) =>
			this.Dispatcher.Dispatch<IClearCachedRolesRequest, ClearCachedRolesRequestParameters, ClearCachedRolesResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityClearCachedRolesDispatch<ClearCachedRolesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClearCachedRolesAsync(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)), cancellationToken);

		/// <inheritdoc/>
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClearCachedRolesRequest, ClearCachedRolesRequestParameters, ClearCachedRolesResponse, IClearCachedRolesResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityClearCachedRolesDispatchAsync<ClearCachedRolesResponse>(p, c)
			);
	}
}
