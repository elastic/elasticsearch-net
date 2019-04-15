using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc />
		ClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request);

		/// <inheritdoc />
		Task<ClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClearCachedRolesResponse ClearCachedRoles(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null) =>
			ClearCachedRoles(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)));

		/// <inheritdoc />
		public ClearCachedRolesResponse ClearCachedRoles(IClearCachedRolesRequest request) =>
			DoRequest<IClearCachedRolesRequest, ClearCachedRolesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClearCachedRolesResponse> ClearCachedRolesAsync(
			Names roles,
			Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken ct = default
		) => ClearCachedRolesAsync(selector.InvokeOrDefault(new ClearCachedRolesDescriptor(roles)), ct);

		/// <inheritdoc />
		public Task<ClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClearCachedRolesRequest, ClearCachedRolesResponse, ClearCachedRolesResponse>(request, request.RequestParameters, ct);
	}
}
