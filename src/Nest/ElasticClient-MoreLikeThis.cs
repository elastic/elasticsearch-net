using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		public IQueryResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			return this.Dispatch<MoreLikeThisDescriptor<T>, MoreLikeThisQueryString, QueryResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					CopySearchQueryString(d);
					return this.RawDispatch.MltDispatch(p, d._Search);
				}

			);
		}
		public Task<IQueryResponse<T>> MoreLikeThisAsync<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			return this.DispatchAsync<MoreLikeThisDescriptor<T>, MoreLikeThisQueryString, QueryResponse<T>, IQueryResponse<T>>(
				mltSelector,
				(p, d) =>
				{
					CopySearchQueryString(d);
					return this.RawDispatch.MltDispatchAsync(p, d._Search);
				});
		}

		private static void CopySearchQueryString<T>(MoreLikeThisDescriptor<T> d) where T : class
		{
			if (d._Search != null)
			{
				var searchQs = d._Search._QueryString.NameValueCollection;
				foreach (var k in searchQs.AllKeys)
					d._QueryString.NameValueCollection[k] = searchQs[k];
			}
		}
	}
}