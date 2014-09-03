using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{
		public ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector) where T : class
		{
			return this.SearchTemplate<T, T>(selector);
		}

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class
		{
			selector.ThrowIfNull("searchSelector");
			var descriptor = selector(new SearchTemplateDescriptor<T>());

			IPathInfo<SearchTemplateRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(descriptor));

			var status = this.RawDispatch.SearchTemplateDispatch<SearchResponse<TResult>>(pathInfo, descriptor);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult>(IElasticsearchResponse response, Stream stream, ISearchTemplateRequest d)
			where T : class
			where TResult : class
		{
			var converter = this.CreateCovariantSearchSelector<T, TResult>(d);
			var dict = response.Success
				? Serializer.DeserializeInternal<SearchResponse<TResult>>(stream, converter)
				: null;
			return dict;
		}

		private Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class
		{

			Func<IElasticsearchResponse, Stream, SearchResponse<TResult>> responseCreator =
					(r, s) => this.FieldsSearchDeserializer<T, TResult>(r, s, request);
			return responseCreator;
		}

		private JsonConverter CreateCovariantSearchSelector<T, TResult>(ISearchTemplateRequest originalSearchDescriptor)
			where T : class
			where TResult : class
		{
			SearchTemplatePathInfo.CloseOverAutomagicCovariantResultSelector(this.Infer, originalSearchDescriptor);
			return originalSearchDescriptor.TypeSelector == null ? null : new ConcreteTypeConverter<TResult>(originalSearchDescriptor.TypeSelector);
		}
	}
}
