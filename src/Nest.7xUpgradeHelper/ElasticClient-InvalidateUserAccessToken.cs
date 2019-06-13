using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client,string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client,IInvalidateUserAccessTokenRequest request);

		/// <inheritdoc />
		public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client,string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client,IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

}
