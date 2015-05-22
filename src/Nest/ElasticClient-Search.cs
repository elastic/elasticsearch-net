using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector, string plugin = "_search") where T : class
		{
            return this.Search<T, T>(searchSelector, plugin);
		}

		/// <inheritdoc />
        public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector, string plugin = "_search")
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());

			IPathInfo<SearchRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(descriptor));

            var status = this.RawDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, descriptor, plugin);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}



        public ISearchResponse<T> Search<T>(ISearchRequest request, string plugin = "_search")
			where T : class
		{
            return this.Search<T, T>(request, plugin);
		}

        public ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request, string plugin = "_search")
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));

            var status = this.RawDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, request, plugin);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		/// <inheritdoc />
        public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector, string plugin = "_search")
			where T : class
		{
            return this.SearchAsync<T, T>(searchSelector, plugin);
		}

		/// <inheritdoc />
        public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector, string plugin = "_search")
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			
			IPathInfo<SearchRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(_connectionSettings)
				.DeserializationState(CreateSearchDeserializer<T, TResult>(descriptor));

            return this.RawDispatch.SearchDispatchAsync<SearchResponse<TResult>>(pathInfo, descriptor, plugin)
				.ContinueWith<ISearchResponse<TResult>>(t => {
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;
					
					return t.Result.Success
						? t.Result.Response
						: CreateInvalidInstance<SearchResponse<TResult>>(t.Result);
				});
		}


        public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request, string plugin = "_search")
			where T : class
		{
            return this.SearchAsync<T, T>(request, plugin);
		}

        public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request, string plugin = "_search")
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));

            return this.RawDispatch.SearchDispatchAsync<SearchResponse<TResult>>(pathInfo, request, plugin)
				.ContinueWith<ISearchResponse<TResult>>(t => {
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;
					
					return t.Result.Success
						? t.Result.Response
						: CreateInvalidInstance<SearchResponse<TResult>>(t.Result);
				});
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult>(IElasticsearchResponse response, Stream stream, ISearchRequest d)
			where T : class
			where TResult : class
		{
			var converter = this.CreateCovariantSearchSelector<T, TResult>(d);
			var dict = response.Success
				? Serializer.DeserializeInternal<SearchResponse<TResult>>(stream, converter)
				: null;
			return dict;
		}
		
		private Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class
		{
				
			Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> responseCreator = 
					(r, s) => this.FieldsSearchDeserializer<T, TResult>(r, s, request);
			return responseCreator;
		}
		
		private JsonConverter CreateCovariantSearchSelector<T, TResult>(ISearchRequest originalSearchDescriptor)
			where T : class
			where TResult : class
		{
			SearchPathInfo.CloseOverAutomagicCovariantResultSelector(this.Infer, originalSearchDescriptor);
			return originalSearchDescriptor.TypeSelector == null ? null : new ConcreteTypeConverter<TResult>(originalSearchDescriptor.TypeSelector);
		}
	}
}