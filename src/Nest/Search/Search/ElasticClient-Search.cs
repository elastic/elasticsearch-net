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
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class
		{
			return this.Search<T, T>(searchSelector);
		}

		/// <inheritdoc />
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());

			IPathInfo<SearchRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(ConnectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(descriptor));

			var status = this.LowLevelDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, descriptor);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}


		
		public ISearchResponse<T> Search<T>(ISearchRequest request)
			where T : class
		{
			return this.Search<T, T>(request);
		}

		public ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(ConnectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));

			var status = this.LowLevelDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, request);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class
		{
			return this.SearchAsync<T, T>(searchSelector);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			
			IPathInfo<SearchRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(ConnectionSettings)
				.DeserializationState(CreateSearchDeserializer<T, TResult>(descriptor));

			return this.LowLevelDispatch.SearchDispatchAsync<SearchResponse<TResult>>(pathInfo, descriptor)
				.ContinueWith<ISearchResponse<TResult>>(t => {
					if (t.IsFaulted && t.Exception != null)
					{
						t.Exception.Flatten().InnerException.RethrowKeepingStackTrace();
						return null; //won't be hit
					}
					
					return t.Result.Success
						? t.Result.Response
						: CreateInvalidInstance<SearchResponse<TResult>>(t.Result);
				});
		}


		public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)
			where T : class
		{
			return this.SearchAsync<T, T>(request);
		}

		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(ConnectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));
			
			return this.LowLevelDispatch.SearchDispatchAsync<SearchResponse<TResult>>(pathInfo, request)
				.ContinueWith<ISearchResponse<TResult>>(t => {
					if (t.IsFaulted && t.Exception != null)
					{
						t.Exception.Flatten().InnerException.RethrowKeepingStackTrace();
						return null; //won't be hit
					}
					
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
				? new NestSerializer(this.ConnectionSettings, converter).Deserialize<SearchResponse<TResult>>(stream)
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