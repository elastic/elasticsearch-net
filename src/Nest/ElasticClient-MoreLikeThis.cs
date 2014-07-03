using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector)
			where T : class
		{
			return this.Dispatch<MoreLikeThisDescriptor<T>, MoreLikeThisRequestParameters, SearchResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					IMoreLikeThisRequest r = d;
					CopySearchRequestParameters(d);
					return this.RawDispatch.MltDispatch<SearchResponse<T>>(p, r.Search);
				}
			);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<T>> MoreLikeThisAsync<T>(
			Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			return this.DispatchAsync<MoreLikeThisDescriptor<T>, MoreLikeThisRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					IMoreLikeThisRequest r = d;
					CopySearchRequestParameters(d);
					return this.RawDispatch.MltDispatchAsync<SearchResponse<T>>(p, r.Search);
				}
			);
		}

		private static void CopySearchRequestParameters(IMoreLikeThisRequest request) 
		{
			if (request.Search == null) return;
			request.RequestParameters.CopyQueryStringValuesFrom(request.Search.QueryString);
		}
	}
}