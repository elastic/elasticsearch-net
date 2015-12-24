using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

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
					return this.RawDispatch.ScrollDispatch<SearchResponse<T>>(p, scrollId);
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
					return this.RawDispatch.ScrollDispatch<SearchResponse<T>>(p, scrollId);
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
					return this.RawDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, scrollId);
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
				    p.DeserializationState(CreateScrollDeserializer<T, T>(d));

                    return this.RawDispatch.ScrollDispatchAsync<SearchResponse<T>>(p, scrollId);
				}
			);
		}
		


		/// <inheritdoc />
		public IEmptyResponse ClearScroll(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector)
		{
			return this.Dispatcher.Dispatch<ClearScrollDescriptor, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollSelector,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.RawDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);
		}

		/// <inheritdoc />
		public IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest)
		{
			return this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.RawDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);
		}

		/// <inheritdoc />
		public Task<IEmptyResponse> ClearScrollAsync(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector)
		{
			return this.Dispatcher.DispatchAsync<ClearScrollDescriptor, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollSelector,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.RawDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);
		}

		/// <inheritdoc />
		public Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest)
		{
			return this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.RawDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);
		}

		private static string PatchClearScroll(ElasticsearchPathInfo<ClearScrollRequestParameters> p)
		{
			string body = null;
			var scrollId = p.ScrollId;
			if (scrollId != null && scrollId != "_all")
			{
				p.ScrollId = null;
				body = scrollId;
			}
			return body;
		}

        private SearchResponse<TResult> FieldsScrollDeserializer<T, TResult>(IElasticsearchResponse response, Stream stream, IScrollRequest d)
            where T : class
            where TResult : class
        {
            var converter = d.TypeSelector == null ? null : new ConcreteTypeConverter<TResult>(d.TypeSelector);
            var dict = response.Success
                ? Serializer.DeserializeInternal<SearchResponse<TResult>>(stream, converter)
                : null;
            return dict;
        }

        private Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> CreateScrollDeserializer<T, TResult>(IScrollRequest request)
            where T : class
            where TResult : class
        {

            Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> responseCreator =
                    (r, s) => this.FieldsScrollDeserializer<T, TResult>(r, s, request);
            return responseCreator;
        }
    }
}