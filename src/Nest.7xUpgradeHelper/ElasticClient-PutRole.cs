using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.PutRole(), please update this usage.")]
		public static PutRoleResponse PutRole(this IElasticClient client, Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null)
			=> client.Security.PutRole(role, selector);

		[Obsolete("Moved to client.Security.PutRole(), please update this usage.")]
		public static PutRoleResponse PutRole(this IElasticClient client, IPutRoleRequest request)
			=> client.Security.PutRole(request);

		[Obsolete("Moved to client.Security.PutRoleAsync(), please update this usage.")]
		public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client, Name role,
			Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleAsync(role, selector, ct);

		[Obsolete("Moved to client.Security.PutRoleAsync(), please update this usage.")]
		public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client, IPutRoleRequest request, CancellationToken ct = default)
			=> client.Security.PutRoleAsync(request, ct);
	}
}
