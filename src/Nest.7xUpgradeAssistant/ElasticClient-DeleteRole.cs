using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.DeleteRole(), please update this usage.")]
		public static DeleteRoleResponse DeleteRole(this IElasticClient client, Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null
		)
			=> client.Security.DeleteRole(role, selector);

		[Obsolete("Moved to client.Security.DeleteRole(), please update this usage.")]
		public static DeleteRoleResponse DeleteRole(this IElasticClient client, IDeleteRoleRequest request)
			=> client.Security.DeleteRole(request);

		[Obsolete("Moved to client.Security.DeleteRoleAsync(), please update this usage.")]
		public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client, Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeleteRoleAsync(role, selector, ct);

		[Obsolete("Moved to client.Security.DeleteRoleAsync(), please update this usage.")]
		public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client, IDeleteRoleRequest request, CancellationToken ct = default)
			=> client.Security.DeleteRoleAsync(request, ct);
	}
}
