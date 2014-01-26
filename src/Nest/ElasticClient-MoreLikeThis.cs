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
				(p, d) => this.RawDispatch.MltDispatch(p, d)

			);
		}
		public Task<IQueryResponse<T>> MoreLikeThisAsync<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			return this.DispatchAsync<MoreLikeThisDescriptor<T>, MoreLikeThisQueryString, QueryResponse<T>, IQueryResponse<T>>(
				mltSelector,
				(p, d) => this.RawDispatch.MltDispatchAsync(p, d)

			);
		}
	}
}