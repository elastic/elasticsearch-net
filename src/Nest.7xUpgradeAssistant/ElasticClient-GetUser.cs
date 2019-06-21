using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetUser(), please update this usage.")]
		public static GetUserResponse GetUser(this IElasticClient client, Func<GetUserDescriptor, IGetUserRequest> selector = null)
			=> client.Security.GetUser(selector);

		[Obsolete("Moved to client.Security.GetUser(), please update this usage.")]
		public static GetUserResponse GetUser(this IElasticClient client, IGetUserRequest request)
			=> client.Security.GetUser(request);

		[Obsolete("Moved to client.Security.GetUserAsync(), please update this usage.")]
		public static Task<GetUserResponse> GetUserAsync(this IElasticClient client, Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAsync(selector, ct);

		[Obsolete("Moved to client.Security.GetUserAsync(), please update this usage.")]
		public static Task<GetUserResponse> GetUserAsync(this IElasticClient client, IGetUserRequest request, CancellationToken ct = default)
			=> client.Security.GetUserAsync(request, ct);
	}
}
