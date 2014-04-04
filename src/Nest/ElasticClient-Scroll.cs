using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchResponse<T> Scroll<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
			where T : class
		{
			return this.Dispatch<ScrollDescriptor<T>, ScrollRequestParameters, SearchResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.RawDispatch.ScrollDispatch<SearchResponse<T>>(p, scrollId);
				}
			);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<T>> ScrollAsync<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
			where T : class
		{
			return this.DispatchAsync<ScrollDescriptor<T>, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.RawDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, scrollId);
				}
			);
		}
	}
}