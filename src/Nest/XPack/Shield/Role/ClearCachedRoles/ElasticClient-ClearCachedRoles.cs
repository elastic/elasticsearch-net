using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc/>
		IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request);

		/// <inheritdoc/>
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc/>
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request);
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
				(p, d) =>this.LowLevelDispatch.ShieldClearCachedRolesDispatch<ClearCachedRolesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null) =>
			this.ClearCachedRolesAsync(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)));

		/// <inheritdoc/>
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request) =>
			this.Dispatcher.DispatchAsync<IClearCachedRolesRequest, ClearCachedRolesRequestParameters, ClearCachedRolesResponse, IClearCachedRolesResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldClearCachedRolesDispatchAsync<ClearCachedRolesResponse>(p)
			);
	}
}
