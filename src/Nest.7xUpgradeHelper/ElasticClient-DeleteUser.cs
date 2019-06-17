using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteUserResponse DeleteUser(this IElasticClient client, Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null
		)
			=> client.Security.DeleteUser(username, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteUserResponse DeleteUser(this IElasticClient client, IDeleteUserRequest request)
			=> client.Security.DeleteUser(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client, Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeleteUserAsync(username, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client, IDeleteUserRequest request, CancellationToken ct = default)
			=> client.Security.DeleteUserAsync(request, ct);
	}
}
