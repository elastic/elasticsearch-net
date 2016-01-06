using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The search API allows to execute a search query and get back search hits that match the query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-search.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class;

		/// <inheritdoc/>
		ISearchResponse<T> Search<T>(ISearchRequest request) where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request) where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class =>
			this.Search<T, T>(selector);

		/// <inheritdoc/>
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class =>
			this.Search<TResult>(selector.InvokeOrDefault(new SearchDescriptor<T>()));

		/// <inheritdoc/>
		public ISearchResponse<T> Search<T>(ISearchRequest request) where T : class => 
			this.Search<T, T>(request);

		/// <inheritdoc/>
		public ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class =>
			this.Dispatcher.Dispatch<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>>(
				request,
				(p, d) => this.LowLevelDispatch.SearchDispatch<SearchResponse<TResult>>(
					this.CovariantConverterWhenNeeded<T, TResult, ISearchRequest, SearchRequestParameters>(p.RouteValues, request), d
					)
				);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class => 
			this.SearchAsync<T, T>(selector);

		/// <inheritdoc/>
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class =>
			this.SearchAsync<TResult>(selector.InvokeOrDefault(new SearchDescriptor<T>()));

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request) where T : class => 
			this.SearchAsync<T, T>(request);

		/// <inheritdoc/>
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class =>
			this.Dispatcher.DispatchAsync<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>, ISearchResponse<TResult>>(
				request,
				(p, d) => this.LowLevelDispatch.SearchDispatchAsync<SearchResponse<TResult>>(
					this.CovariantConverterWhenNeeded<T, TResult, ISearchRequest, SearchRequestParameters>(p.RouteValues, request), d
					)
				);

	}
}