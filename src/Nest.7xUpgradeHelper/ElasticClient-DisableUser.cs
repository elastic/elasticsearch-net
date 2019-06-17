using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DisableUserResponse DisableUser(this IElasticClient client, Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null
		)
			=> client.Security.DisableUser(username, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DisableUserResponse DisableUser(this IElasticClient client, IDisableUserRequest request)
			=> client.Security.DisableUser(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client, Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DisableUserAsync(username, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client, IDisableUserRequest request,
			CancellationToken ct = default
		)
			=> client.Security.DisableUserAsync(request, ct);
	}
}
