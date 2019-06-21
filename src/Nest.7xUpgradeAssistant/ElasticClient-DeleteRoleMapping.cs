using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.DeleteRoleMapping(), please update this usage.")]
		public static DeleteRoleMappingResponse DeleteRoleMapping(this IElasticClient client, Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null
		)
			=> client.Security.DeleteRoleMapping(role, selector);

		[Obsolete("Moved to client.Security.DeleteRoleMapping(), please update this usage.")]
		public static DeleteRoleMappingResponse DeleteRoleMapping(this IElasticClient client, IDeleteRoleMappingRequest request)
			=> client.Security.DeleteRoleMapping(request);

		[Obsolete("Moved to client.Security.DeleteRoleMappingAsync(), please update this usage.")]
		public static Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(this IElasticClient client, Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeleteRoleMappingAsync(role, selector, ct);

		[Obsolete("Moved to client.Security.DeleteRoleMappingAsync(), please update this usage.")]
		public static Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(this IElasticClient client, IDeleteRoleMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Security.DeleteRoleMappingAsync(request, ct);
	}
}
