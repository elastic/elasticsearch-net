using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetPrivileges(), please update this usage.")]
		public static GetPrivilegesResponse GetPrivileges(this IElasticClient client,
			Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null
		)
			=> client.Security.GetPrivileges(null, selector);

		[Obsolete("Moved to client.Security.GetPrivileges(), please update this usage.")]
		public static GetPrivilegesResponse GetPrivileges(this IElasticClient client, IGetPrivilegesRequest request)
			=> client.Security.GetPrivileges(request);

		[Obsolete("Moved to client.Security.GetPrivilegesAsync(), please update this usage.")]
		public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client,
			Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetPrivilegesAsync(null, selector, ct);

		[Obsolete("Moved to client.Security.GetPrivilegesAsync(), please update this usage.")]
		public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client, IGetPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetPrivilegesAsync(request, ct);
	}
}
