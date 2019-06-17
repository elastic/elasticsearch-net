using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetRole(), please update this usage.")]
		public static GetRoleResponse GetRole(this IElasticClient client, Func<GetRoleDescriptor, IGetRoleRequest> selector = null)
			=> client.Security.GetRole(null, selector);

		[Obsolete("Moved to client.Security.GetRole(), please update this usage.")]
		public static GetRoleResponse GetRole(this IElasticClient client, IGetRoleRequest request)
			=> client.Security.GetRole(request);

		[Obsolete("Moved to client.Security.GetRoleAsync(), please update this usage.")]
		public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client, Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleAsync(null, selector, ct);

		[Obsolete("Moved to client.Security.GetRoleAsync(), please update this usage.")]
		public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client, IGetRoleRequest request, CancellationToken ct = default)
			=> client.Security.GetRoleAsync(request, ct);
	}
}
