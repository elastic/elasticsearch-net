using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRoleResponse DeleteRole(this IElasticClient client, Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null
		)
			=> client.Security.DeleteRole(role, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRoleResponse DeleteRole(this IElasticClient client, IDeleteRoleRequest request)
			=> client.Security.DeleteRole(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client, Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeleteRoleAsync(role, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client, IDeleteRoleRequest request, CancellationToken ct = default)
			=> client.Security.DeleteRoleAsync(request, ct);
	}
}
