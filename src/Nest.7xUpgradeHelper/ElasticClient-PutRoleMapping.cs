using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client, Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null
		)
			=> client.Security.PutRoleMapping(role, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client, IPutRoleMappingRequest request)
			=> client.Security.PutRoleMapping(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client, Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleMappingAsync(role, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client, IPutRoleMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleMappingAsync(request, ct);
	}
}
