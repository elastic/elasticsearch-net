using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserResponse GetUser(this IElasticClient client, Func<GetUserDescriptor, IGetUserRequest> selector = null)
			=> client.Security.GetUser(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserResponse GetUser(this IElasticClient client, IGetUserRequest request)
			=> client.Security.GetUser(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserResponse> GetUserAsync(this IElasticClient client, Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserResponse> GetUserAsync(this IElasticClient client, IGetUserRequest request, CancellationToken ct = default)
			=> client.Security.GetUserAsync(request, ct);
	}
}
