using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return this.Search<T, T>(searchSelector);
		}

		/// <inheritdoc />
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			var pathInfo =
				((IPathInfo<SearchRequestParameters>)descriptor).ToPathInfo(_connectionSettings)
				.DeserializationState(new Func<IElasticsearchResponse, Stream, SearchResponse<TResult>>((r, s) =>
					this.FieldsSearchDeserializer<T, TResult>(r, s, descriptor)
				));

			var status = this.RawDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, descriptor);
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
			var pathInfo = ((IPathInfo<SearchRequestParameters>)request).ToPathInfo(_connectionSettings);

			var status = this.RawDispatch.SearchDispatch<SearchResponse<TResult>>(pathInfo, request);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult>(
			IElasticsearchResponse response, Stream stream, SearchDescriptor<T> d)
			where T : class
			where TResult : class
		{
			var converter = this.CreateCovariantSearchSelector<T, TResult>(d);
			var dict = response.Success
				? Serializer.DeserializeInternal<SearchResponse<TResult>>(stream, converter)
				: null;
			return dict;
		}

		/// <inheritdoc />
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
		{
			return this.SearchAsync<T, T>(searchSelector);
		}

		/// <inheritdoc />
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(
			Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			var deserializationState = this.CreateCovariantSearchSelector<T, TResult>(descriptor);
			var pathInfo =
				((IPathInfo<SearchRequestParameters>) descriptor).ToPathInfo(_connectionSettings)
				.DeserializationState(deserializationState);

			return this.RawDispatch.SearchDispatchAsync<SearchResponse<TResult>>(pathInfo, descriptor)
				.ContinueWith<ISearchResponse<TResult>>(t => {
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;
					
					return t.Result.Success
						? t.Result.Response
						: CreateInvalidInstance<SearchResponse<TResult>>(t.Result);
				});
		}

		private JsonConverter CreateCovariantSearchSelector<T, TResult>(SearchDescriptor<T> originalSearchDescriptor)
			where T : class
			where TResult : class
		{
			var selector = originalSearchDescriptor.CreateCovarianceSelector<TResult>(this.Infer);
			return selector == null ? null : new ConcreteTypeConverter<TResult>(selector);
		}
	}
}