using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutUserResponse PutUser(this IElasticClient client, Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null)
			=> client.Security.PutUser(username, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutUserResponse PutUser(this IElasticClient client, IPutUserRequest request)
			=> client.Security.PutUser(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutUserResponse> PutUserAsync(this IElasticClient client, Name username,
			Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.PutUserAsync(username, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutUserResponse> PutUserAsync(this IElasticClient client, IPutUserRequest request, CancellationToken ct = default)
			=> client.Security.PutUserAsync(request, ct);
	}
}
