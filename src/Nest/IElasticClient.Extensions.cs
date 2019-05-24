using System;
using System.Threading.Tasks;
using Nest.Specification.SecurityApi;

namespace Nest
{
	//TODO 7.0 these are extensions for cases  where the descriptor has a constructor which forces some parameters in the body to be set.
	//It'd be nice if the generator picks these up but not worth the investment right now.
	
	public static class ElasticClientExtensions
	{
		///<inheritdoc cref="IScrollRequest"/>
		public static SearchResponse<TDocument> Scroll<TDocument>(this IElasticClient client,
			string scrollTime, string scrollId, Func<ScrollDescriptor<TDocument>, IScrollRequest> selector = null
		)
			where TDocument : class => client.Scroll<TDocument>(f => selector.InvokeOrDefault(f.Scroll(scrollTime).ScrollId(scrollId)));
		
		///<inheritdoc cref="IScrollRequest"/>
		public static Task<SearchResponse<TDocument>> ScrollAsync<TDocument>(this IElasticClient client,
			string scrollTime, string scrollId, Func<ScrollDescriptor<TDocument>, IScrollRequest> selector = null
		)
			where TDocument : class => client.ScrollAsync<TDocument>(f => selector.InvokeOrDefault(f.Scroll(scrollTime).ScrollId(scrollId)));
		
		///<inheritdoc cref="IGetUserAccessTokenRequest"/>
		public static GetUserAccessTokenResponse GetUserAccessToken(this SecurityNamespace client,
			string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		) => client.GetUserAccessToken(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));
		
		///<inheritdoc cref="IGetUserAccessTokenRequest"/>
		public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this SecurityNamespace client,
			string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		) => client.GetUserAccessTokenAsync(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));
		
		///<inheritdoc cref="IInvalidateUserAccessTokenRequest"/>
		public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this SecurityNamespace client,
			string token, Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		) => client.InvalidateUserAccessToken(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)));
		
		///<inheritdoc cref="IInvalidateUserAccessTokenRequest"/>
		public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this SecurityNamespace client,
			string token, Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		) => client.InvalidateUserAccessTokenAsync(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)));

	}
}
