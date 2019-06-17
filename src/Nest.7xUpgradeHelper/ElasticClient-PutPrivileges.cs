using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.PutPrivileges(), please update this usage.")]
		public static PutPrivilegesResponse PutPrivileges(this IElasticClient client, Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector)
			=> client.Security.PutPrivileges(selector);

		[Obsolete("Moved to client.Security.PutPrivileges(), please update this usage.")]
		public static PutPrivilegesResponse PutPrivileges(this IElasticClient client, IPutPrivilegesRequest request)
			=> client.Security.PutPrivileges(request);

		[Obsolete("Moved to client.Security.PutPrivilegesAsync(), please update this usage.")]
		public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client,
			Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken ct = default
		)
			=> client.Security.PutPrivilegesAsync(selector, ct);

		[Obsolete("Moved to client.Security.PutPrivilegesAsync(), please update this usage.")]
		public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client, IPutPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.PutPrivilegesAsync(request, ct);
	}
}
