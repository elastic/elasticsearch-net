using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. 
		/// <para>The scroll parameter is a time value parameter (for example: scroll=5m), 
		/// indicating for how long the nodes that participate in the search will maintain relevant resources in
		/// order to continue and support it.</para><para> 
		/// This is very similar in its idea to opening a cursor against a database.</para>
		/// <para> </para><para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html</para>
		/// </summary>
		/// <typeparam name="T">The type that represents the result hits</typeparam>
		/// <param name="scrollRequest">A descriptor that describes the scroll operation</param>
		/// <returns>A query response holding T hits as well as the ScrollId for the next scroll operation</returns>
		ISearchResponse<T> Scroll<T>(IScrollRequest scrollRequest) where T : class;

		//TODO swapping these parameters around would cause a hard to spot bug when upgrading to 2.0 discuss with @gmarz
		///<inheritdoc/>
		ISearchResponse<T> Scroll<T>(TimeUnitExpression scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> scrollSelector = null) 
			where T : class;

		///<inheritdoc/>
		Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest scrollRequest)
			where T : class;

		///<inheritdoc/>
		Task<ISearchResponse<T>> ScrollAsync<T>(TimeUnitExpression scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> scrollSelector = null)
			where T : class;

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISearchResponse<T> Scroll<T>(IScrollRequest request) where T : class => 
			this.Dispatcher.Dispatch<IScrollRequest, ScrollRequestParameters, SearchResponse<T>>(
				request,
				(p, d) =>
				{
					var id = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatch<SearchResponse<T>>(p, id);
				}
			);

		/// <inheritdoc/>
		public ISearchResponse<T> Scroll<T>(TimeUnitExpression scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> scrollSelector = null) where T : class => 
			this.Dispatcher.Dispatch<IScrollRequest, ScrollRequestParameters, SearchResponse<T>>(
				scrollSelector.InvokeOrDefault(new ScrollDescriptor<T>().Scroll(scrollTime).ScrollId(scrollId)),
				(p, d) =>
				{
					var id = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatch<SearchResponse<T>>(p, id);
				}
			);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request) where T : class => 
			this.Dispatcher.DispatchAsync<IScrollRequest, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				request,
				(p, d) =>
				{
					var id = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, id);
				}
			);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> ScrollAsync<T>(TimeUnitExpression scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> scrollSelector = null) where T : class => 
			this.Dispatcher.DispatchAsync<IScrollRequest, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				scrollSelector.InvokeOrDefault(new ScrollDescriptor<T>().Scroll(scrollTime).ScrollId(scrollId)),
				(p, d) =>
				{
					var id = p.ScrollId;
					p.ScrollId = null;
					return this.LowLevelDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, id);
				}
			);
	}
}