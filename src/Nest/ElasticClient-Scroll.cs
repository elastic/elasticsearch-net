using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		public IQueryResponse<T> Scroll<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) 
			where T : class
		{
			return this.Dispatch<ScrollDescriptor<T>, ScrollQueryString, QueryResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					var scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.RawDispatch.ScrollDispatch<QueryResponse<T>>(p, scrollId);
				}
			);
		}
		public Task<IQueryResponse<T>> ScrollAsync<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) 
			where T : class
		{
			return this.DispatchAsync<ScrollDescriptor<T>, ScrollQueryString, QueryResponse<T>, IQueryResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					var scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.RawDispatch.ScrollDispatchAsync<QueryResponse<T>>(p, scrollId);
				}
			);
		}
	}
}