using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetRoleMapping(), please update this usage.")]
		public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client,
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null
		)
			=> client.Security.GetRoleMapping(null, selector);

		[Obsolete("Moved to client.Security.GetRoleMapping(), please update this usage.")]
		public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client, IGetRoleMappingRequest request)
			=> client.Security.GetRoleMapping(request);

		[Obsolete("Moved to client.Security.GetRoleMappingAsync(), please update this usage.")]
		public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client,
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleMappingAsync(null, selector, ct);

		[Obsolete("Moved to client.Security.GetRoleMappingAsync(), please update this usage.")]
		public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client, IGetRoleMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetRoleMappingAsync(request, ct);
	}
}
