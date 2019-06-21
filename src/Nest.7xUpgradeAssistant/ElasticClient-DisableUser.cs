using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.DisableUser(), please update this usage.")]
		public static DisableUserResponse DisableUser(this IElasticClient client, Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null
		)
			=> client.Security.DisableUser(username, selector);

		[Obsolete("Moved to client.Security.DisableUser(), please update this usage.")]
		public static DisableUserResponse DisableUser(this IElasticClient client, IDisableUserRequest request)
			=> client.Security.DisableUser(request);

		[Obsolete("Moved to client.Security.DisableUserAsync(), please update this usage.")]
		public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client, Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DisableUserAsync(username, selector, ct);

		[Obsolete("Moved to client.Security.DisableUserAsync(), please update this usage.")]
		public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client, IDisableUserRequest request,
			CancellationToken ct = default
		)
			=> client.Security.DisableUserAsync(request, ct);
	}
}
