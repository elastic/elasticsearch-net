using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRoleResponse GetRole(this IElasticClient client, Func<GetRoleDescriptor, IGetRoleRequest> selector = null)
			=> client.Security.GetRole(null, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRoleResponse GetRole(this IElasticClient client, IGetRoleRequest request)
			=> client.Security.GetRole(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client, Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleAsync(null, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client, IGetRoleRequest request, CancellationToken ct = default)
			=> client.Security.GetRoleAsync(request, ct);
	}
}
