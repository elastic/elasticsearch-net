using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client,
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null
		)
			=> client.Security.GetRoleMapping(null, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client, IGetRoleMappingRequest request)
			=> client.Security.GetRoleMapping(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client,
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleMappingAsync(null, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client, IGetRoleMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleMappingAsync(request, ct);
	}
}
