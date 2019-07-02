using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.HasPrivileges(), please update this usage.")]
		public static HasPrivilegesResponse HasPrivileges(this IElasticClient client,
			Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null
		)
			=> client.Security.HasPrivileges(selector);

		[Obsolete("Moved to client.Security.HasPrivileges(), please update this usage.")]
		public static HasPrivilegesResponse HasPrivileges(this IElasticClient client, IHasPrivilegesRequest request)
			=> client.Security.HasPrivileges(request);

		[Obsolete("Moved to client.Security.HasPrivilegesAsync(), please update this usage.")]
		public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client,
			Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.HasPrivilegesAsync(selector, ct);

		[Obsolete("Moved to client.Security.HasPrivilegesAsync(), please update this usage.")]
		public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client, IHasPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.HasPrivilegesAsync(request, ct);
	}
}
