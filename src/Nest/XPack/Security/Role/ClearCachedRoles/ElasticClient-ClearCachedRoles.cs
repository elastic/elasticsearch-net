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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null) =>
			ClearCachedRoles(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)));

		/// <inheritdoc />
		public IClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request) =>
			DoRequest<IClearCachedRolesRequest, ClearCachedRolesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(
			Names roles,
			Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken ct = default
		) => ClearCachedRolesAsync(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)), ct);

		/// <inheritdoc />
		public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClearCachedRolesRequest, IClearCachedRolesResponse, ClearCachedRolesResponse>(request, request.RequestParameters, ct);
	}
}
