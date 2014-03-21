using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IQueryResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector)
			where T : class
		{
			return this.Dispatch<MoreLikeThisDescriptor<T>, MoreLikeThisQueryString, QueryResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					CopySearchQueryString(d);
					return this.RawDispatch.MltDispatch<QueryResponse<T>>(p, d._Search);
				}
			);
		}

		/// <inheritdoc />
		public Task<IQueryResponse<T>> MoreLikeThisAsync<T>(
			Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			return this.DispatchAsync<MoreLikeThisDescriptor<T>, MoreLikeThisQueryString, QueryResponse<T>, IQueryResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					CopySearchQueryString(d);
					return this.RawDispatch.MltDispatchAsync<QueryResponse<T>>(p, d._Search);
				}
			);
		}

		private static void CopySearchQueryString<T>(MoreLikeThisDescriptor<T> d) where T : class
		{
			if (d._Search == null) return;
			var searchQs = d._Search._QueryString._QueryStringDictionary;
			foreach (var k in searchQs.Keys)
				d._QueryString._QueryStringDictionary[k] = searchQs[k];
		}
	}
}