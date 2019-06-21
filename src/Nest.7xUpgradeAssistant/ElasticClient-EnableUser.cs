using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.EnableUser(), please update this usage.")]
		public static EnableUserResponse EnableUser(this IElasticClient client, Name username,
			Func<EnableUserDescriptor, IEnableUserRequest> selector = null
		)
			=> client.Security.EnableUser(username, selector);

		[Obsolete("Moved to client.Security.EnableUser(), please update this usage.")]
		public static EnableUserResponse EnableUser(this IElasticClient client, IEnableUserRequest request)
			=> client.Security.EnableUser(request);

		[Obsolete("Moved to client.Security.EnableUserAsync(), please update this usage.")]
		public static Task<EnableUserResponse> EnableUserAsync(this IElasticClient client, Name username,
			Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.EnableUserAsync(username, selector, ct);

		[Obsolete("Moved to client.Security.EnableUserAsync(), please update this usage.")]
		public static Task<EnableUserResponse> EnableUserAsync(this IElasticClient client, IEnableUserRequest request, CancellationToken ct = default)
			=> client.Security.EnableUserAsync(request, ct);
	}
}
