using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			var status = this.LowLevelDispatch.SearchTemplateDispatch<SearchResponse<TResult>>(pathInfo, descriptor);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		public ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request) where T : class
		{
			return this.SearchTemplate<T, T>(request);
		}

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));

			var status = this.LowLevelDispatch.SearchTemplateDispatch<SearchResponse<TResult>>(pathInfo, request);
			return status.Success ? status.Response : CreateInvalidInstance<SearchResponse<TResult>>(status);
		}

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector) where T : class
		{
			return this.SearchTemplateAsync<T, T>(selector);
		}

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new SearchTemplateDescriptor<T>());

			IPathInfo<SearchTemplateRequestParameters> p = descriptor;
			var pathInfo = p
				.ToPathInfo(_connectionSettings)
				.DeserializationState(CreateSearchDeserializer<T, TResult>(descriptor));

			return this.LowLevelDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>(pathInfo, descriptor)
				.ContinueWith<ISearchResponse<TResult>>(t =>
				{
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

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request) where T : class
		{
			return this.SearchTemplateAsync<T, T>(request);
		}

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class
		{
			var pathInfo = request
				.ToPathInfo(_connectionSettings)
				.DeserializationState(this.CreateSearchDeserializer<T, TResult>(request));

			return this.LowLevelDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>(pathInfo, request)
				.ContinueWith<ISearchResponse<TResult>>(t =>
				{
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
