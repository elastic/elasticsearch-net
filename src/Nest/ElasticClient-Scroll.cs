using System;
using System.Threading.Tasks;

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
				(p, d) => this.RawDispatch.ScrollDispatch(p, p.ScrollId)
			);
		}
		public Task<IQueryResponse<T>> ScrollAsync<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) 
			where T : class
		{
			return this.DispatchAsync<ScrollDescriptor<T>, ScrollQueryString, QueryResponse<T>, IQueryResponse<T>>(
				scrollSelector,
				(p, d) => this.RawDispatch.ScrollDispatchAsync(p, p.ScrollId)
			);
		}
	}
}