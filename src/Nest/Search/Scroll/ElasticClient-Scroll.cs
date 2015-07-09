using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchResponse<T> Scroll<T>(IScrollRequest request) where T : class
		{
			return this.Dispatcher.Dispatch<IScrollRequest, ScrollRequestParameters, SearchResponse<T>>(
				request,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatch<SearchResponse<T>>(p, scrollId);
				}
			);
		}
		/// <inheritdoc />
		public ISearchResponse<T> Scroll<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) where T : class
		{
			return this.Dispatcher.Dispatch<ScrollDescriptor<T>, ScrollRequestParameters, SearchResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatch<SearchResponse<T>>(p, scrollId);
				}
			);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request) where T : class
		{
			return this.Dispatcher.DispatchAsync<IScrollRequest, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				request,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, scrollId);
				}
			);
		}
		
		/// <inheritdoc />
		public Task<ISearchResponse<T>> ScrollAsync<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) where T : class
		{
			return this.Dispatcher.DispatchAsync<ScrollDescriptor<T>, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				scrollSelector,
				(p, d) =>
				{
					string scrollId = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, scrollId);
				}
			);
		}

	}
}