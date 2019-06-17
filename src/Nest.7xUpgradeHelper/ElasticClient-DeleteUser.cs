using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.DeleteUser(), please update this usage.")]
		public static DeleteUserResponse DeleteUser(this IElasticClient client, Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null
		)
			=> client.Security.DeleteUser(username, selector);

		[Obsolete("Moved to client.Security.DeleteUser(), please update this usage.")]
		public static DeleteUserResponse DeleteUser(this IElasticClient client, IDeleteUserRequest request)
			=> client.Security.DeleteUser(request);

		[Obsolete("Moved to client.Security.DeleteUserAsync(), please update this usage.")]
		public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client, Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeleteUserAsync(username, selector, ct);

		[Obsolete("Moved to client.Security.DeleteUserAsync(), please update this usage.")]
		public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client, IDeleteUserRequest request, CancellationToken ct = default)
			=> client.Security.DeleteUserAsync(request, ct);
	}
}
