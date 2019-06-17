using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutRoleResponse PutRole(this IElasticClient client, Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null)
			=> client.Security.PutRole(role, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutRoleResponse PutRole(this IElasticClient client, IPutRoleRequest request)
			=> client.Security.PutRole(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client, Name role,
			Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleAsync(role, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client, IPutRoleRequest request, CancellationToken ct = default)
			=> client.Security.PutRoleAsync(request, ct);
	}
}
