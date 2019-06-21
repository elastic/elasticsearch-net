using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.PutRoleMapping(), please update this usage.")]
		public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client, Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null
		)
			=> client.Security.PutRoleMapping(role, selector);

		[Obsolete("Moved to client.Security.PutRoleMapping(), please update this usage.")]
		public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client, IPutRoleMappingRequest request)
			=> client.Security.PutRoleMapping(request);

		[Obsolete("Moved to client.Security.PutRoleMappingAsync(), please update this usage.")]
		public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client, Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleMappingAsync(role, selector, ct);

		[Obsolete("Moved to client.Security.PutRoleMappingAsync(), please update this usage.")]
		public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client, IPutRoleMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Security.PutRoleMappingAsync(request, ct);
	}
}
