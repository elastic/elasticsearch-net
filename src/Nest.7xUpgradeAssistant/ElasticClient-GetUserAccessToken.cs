using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetUserAccessToken(), please update this usage.")]
		public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client, string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		)
			=> client.Security.GetUserAccessToken(username, password, selector);

		[Obsolete("Moved to client.Security.GetUserAccessToken(), please update this usage.")]
		public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client, IGetUserAccessTokenRequest request)
			=> client.Security.GetUserAccessToken(request);

		[Obsolete("Moved to client.Security.GetUserAccessTokenAsync(), please update this usage.")]
		public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client, string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAccessTokenAsync(username, password, selector, ct);

		[Obsolete("Moved to client.Security.GetUserAccessTokenAsync(), please update this usage.")]
		public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client, IGetUserAccessTokenRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAccessTokenAsync(request, ct);
	}
}
