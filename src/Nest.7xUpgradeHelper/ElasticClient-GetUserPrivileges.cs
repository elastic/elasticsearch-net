using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetUserPrivileges(), please update this usage.")]
		public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client,
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null
		)
			=> client.Security.GetUserPrivileges(selector);

		[Obsolete("Moved to client.Security.GetUserPrivileges(), please update this usage.")]
		public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client, IGetUserPrivilegesRequest request)
			=> client.Security.GetUserPrivileges(request);

		[Obsolete("Moved to client.Security.GetUserPrivilegesAsync(), please update this usage.")]
		public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client,
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserPrivilegesAsync(selector, ct);

		[Obsolete("Moved to client.Security.GetUserPrivilegesAsync(), please update this usage.")]
		public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client, IGetUserPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetUserPrivilegesAsync(request, ct);
	}
}
