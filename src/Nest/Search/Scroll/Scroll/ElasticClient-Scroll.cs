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
		/// <param name="request">A descriptor that describes the scroll operation</param>
		/// <returns>A query response holding <typeparamref name="T"/> hits as well as the ScrollId for the next scroll operation</returns>
		ISearchResponse<T> Scroll<T>(IScrollRequest request) where T : class;

		///<inheritdoc/>
		ISearchResponse<T> Scroll<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null) 
			where T : class;

		///<inheritdoc/>
		Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request)
			where T : class;

		///<inheritdoc/>
		Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISearchResponse<T> Scroll<T>(IScrollRequest request) where T : class => 
			this.Dispatcher.Dispatch<IScrollRequest, ScrollRequestParameters, SearchResponse<T>>(
				request,
				(p, d) => this.LowLevelDispatch.ScrollDispatch<SearchResponse<T>>(
					this.CovariantConverterWhenNeeded<T, T, IScrollRequest, ScrollRequestParameters>(p.RouteValues, request), d
				)
			);

		/// <inheritdoc/>
		public ISearchResponse<T> Scroll<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null) where T : class =>
			this.Scroll<T>(selector.InvokeOrDefault(new ScrollDescriptor<T>().Scroll(scrollTime).ScrollId(scrollId)));

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request) where T : class => 
			this.Dispatcher.DispatchAsync<IScrollRequest, ScrollRequestParameters, SearchResponse<T>, ISearchResponse<T>>(
				request,
				(p, d) => this.LowLevelDispatch.ScrollDispatchAsync<SearchResponse<T>>(
					this.CovariantConverterWhenNeeded<T, T, IScrollRequest, ScrollRequestParameters>(p.RouteValues, request), d
				)
			);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null) where T : class => 
			this.ScrollAsync<T>(selector.InvokeOrDefault(new ScrollDescriptor<T>().Scroll(scrollTime).ScrollId(scrollId)));
	}
}