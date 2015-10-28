using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The search API allows to execute a search query and get back search hits that match the query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-search.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="searchSelector">A descriptor that describes the parameters for the search operation</param>
		ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null) where T : class;

		/// <inheritdoc/>
		ISearchResponse<T> Search<T>(ISearchRequest request) where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="searchSelector">A descriptor that describes the parameters for the search operation</param>
		Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null) where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request) where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null)
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
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null) where T : class =>
			this.Search<T, T>(searchSelector);

		/// <inheritdoc/>
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null)
			where T : class
			where TResult : class =>
			this.Search<TResult>(searchSelector.InvokeOrDefault(new SearchDescriptor<T>()));

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
					this.AttachCustomConverterWhenNeeded<T, TResult>(p.RouteValues, request), d
					)
				);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null)
			where T : class => 
			this.SearchAsync<T, T>(searchSelector);

		/// <inheritdoc/>
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector = null)
			where T : class
			where TResult : class =>
			this.SearchAsync<TResult>(searchSelector.InvokeOrDefault(new SearchDescriptor<T>()));

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
					this.AttachCustomConverterWhenNeeded<T, TResult>(p.RouteValues, request), d
					)
				);

		private ISearchRequest AttachCustomConverterWhenNeeded<T, TResult>(RouteValues p, ISearchRequest d)
			where T : class
			where TResult : class
		{
			d.RequestParameters.DeserializationOverride(this.CreateSearchDeserializer<T, TResult>(d));
			return d;
		}

		private Func<IApiCallDetails, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class
		{
			CovariantSearch.CloseOverAutomagicCovariantResultSelector(this.Infer, request);
			if (request.TypeSelector == null) return null;
			return (r, s) => this.FieldsSearchDeserializer<T, TResult>(r, s, request);
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult>(IApiCallDetails response, Stream stream, ISearchRequest d)
			where T : class
			where TResult : class =>
			!response.Success
				? null
				: new NestSerializer(this.ConnectionSettings, new ConcreteTypeConverter<TResult>(d.TypeSelector))
					.Deserialize<SearchResponse<TResult>>(stream);

	}
}