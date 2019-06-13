using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client,string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client,IGetUserAccessTokenRequest request);

		/// <inheritdoc />
		public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client,string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client,IGetUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

}
