using System;
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
					CopySearchRequestParameters(d);
					return this.RawDispatch.MltDispatch<SearchResponse<T>>(p, d._Search);
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
					CopySearchRequestParameters(d);
					return this.RawDispatch.MltDispatchAsync<SearchResponse<T>>(p, d._Search);
				}
			);
		}

		private static void CopySearchRequestParameters<T>(MoreLikeThisDescriptor<T> d) where T : class
		{
			IRequest<MoreLikeThisRequestParameters> request = d;
			if (d._Search == null) return;
			request.RequestParameters.CopyQueryStringValuesFrom(d._Search.QueryString);
		}
	}
}