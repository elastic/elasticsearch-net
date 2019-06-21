using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.ClearCachedRoles(), please update this usage.")]
		public static ClearCachedRolesResponse ClearCachedRoles(this IElasticClient client, Names roles,
			Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null
		)
			=> client.Security.ClearCachedRoles(roles, selector);

		[Obsolete("Moved to client.Security.ClearCachedRoles(), please update this usage.")]
		public static ClearCachedRolesResponse ClearCachedRoles(this IElasticClient client, IClearCachedRolesRequest request)
			=> client.Security.ClearCachedRoles(request);

		[Obsolete("Moved to client.Security.ClearCachedRolesAsync(), please update this usage.")]
		public static Task<ClearCachedRolesResponse> ClearCachedRolesAsync(this IElasticClient client, Names roles,
			Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRolesAsync(roles, selector, ct);

		[Obsolete("Moved to client.Security.ClearCachedRolesAsync(), please update this usage.")]
		public static Task<ClearCachedRolesResponse> ClearCachedRolesAsync(this IElasticClient client, IClearCachedRolesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRolesAsync(request, ct);
	}
}
