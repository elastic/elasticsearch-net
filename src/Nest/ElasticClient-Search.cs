using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

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
			var types =
				(originalSearchDescriptor._Types ?? Enumerable.Empty<TypeNameMarker>()).Where(t => t.Type != null);
			if (originalSearchDescriptor._ConcreteTypeSelector != null || !types.Any(t => t.Type != typeof (TResult)))
				return originalSearchDescriptor._ConcreteTypeSelector == null
					? null
					: new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector);
			
			var typeDictionary = types.ToDictionary(Infer.TypeName, t => t.Type);
			originalSearchDescriptor._ConcreteTypeSelector = (o, h) =>
			{
				Type t;
				return !typeDictionary.TryGetValue(h.Type, out t) ? typeof (TResult) : t;
			};
			return originalSearchDescriptor._ConcreteTypeSelector == null 
				? null 
				: new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector);
		}
	}
}