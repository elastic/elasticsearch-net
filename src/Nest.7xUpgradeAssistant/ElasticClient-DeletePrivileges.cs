using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.DeletePrivileges(), please update this usage.")]
		public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client, Name application, Name name,
			Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null
		)
			=> client.Security.DeletePrivileges(application, name, selector);

		[Obsolete("Moved to client.Security.DeletePrivileges(), please update this usage.")]
		public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client, IDeletePrivilegesRequest request)
			=> client.Security.DeletePrivileges(request);

		[Obsolete("Moved to client.Security.DeletePrivilegesAsync(), please update this usage.")]
		public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client, Name application, Name name,
			Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeletePrivilegesAsync(application, name, selector, ct);

		[Obsolete("Moved to client.Security.DeletePrivilegesAsync(), please update this usage.")]
		public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client, IDeletePrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.DeletePrivilegesAsync(request, ct);
	}
}
