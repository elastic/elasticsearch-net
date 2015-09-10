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
	public partial interface IElasticClient
	{
		/// <summary>
		/// The /_search/template endpoint allows to use the mustache language to pre render search 
		/// requests, before they are executed and fill existing templates with template parameters.
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		/// <returns></returns>
		ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;
	}

	public partial class ElasticClient
	{
		public ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			this.SearchTemplate<T, T>(selector);

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class =>
			this.SearchTemplate<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()));

		public ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request) where T : class =>
			this.SearchTemplate<T, T>(request);

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class =>
			this.Dispatcher.Dispatch<ISearchTemplateRequest, SearchTemplateRequestParameters, SearchResponse<TResult>>(
				request,
				this.CreateSearchDeserializer<T, TResult>(request),
				this.LowLevelDispatch.SearchTemplateDispatch<SearchResponse<TResult>>
			);

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			this.SearchTemplateAsync<T, T>(selector);

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class =>
			this.SearchTemplateAsync<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()));

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request) where T : class =>
			this.SearchTemplateAsync<T, T>(request);

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class =>
			this.Dispatcher.DispatchAsync<ISearchTemplateRequest, SearchTemplateRequestParameters, SearchResponse<TResult>, ISearchResponse<TResult>>(
				request,
				this.CreateSearchDeserializer<T, TResult>(request),
				this.LowLevelDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>
			);

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult>(IApiCallDetails response, Stream stream, ISearchTemplateRequest d)
			where T : class
			where TResult : class
		{
			var converter = this.CreateCovariantSearchSelector<T, TResult>(d);
			var dict = response.Success
				? new NestSerializer(this.ConnectionSettings, converter).Deserialize<SearchResponse<TResult>>(stream)
				: null;
			return dict;
		}

		private Func<IApiCallDetails, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class
		{

			Func<IApiCallDetails, Stream, SearchResponse<TResult>> responseCreator =
					(r, s) => this.FieldsSearchDeserializer<T, TResult>(r, s, request);
			return responseCreator;
		}

		private JsonConverter CreateCovariantSearchSelector<T, TResult>(ISearchTemplateRequest originalSearchDescriptor)
			where T : class
			where TResult : class
		{
			SearchTemplateResponseHelper.CloseOverAutomagicCovariantResultSelector(this.Infer, originalSearchDescriptor);
			return originalSearchDescriptor.TypeSelector == null ? null : new ConcreteTypeConverter<TResult>(originalSearchDescriptor.TypeSelector);
		}

		internal static class SearchTemplateResponseHelper
		{
			/// <summary>
			/// Based on the type information present in this descriptor create method that takes
			/// the returned _source and hit and returns the ClrType it should deserialize too.
			/// This is so that Documents[A] can contain actual instances of subclasses B, C as well.
			/// If you specify types using .Types(typeof(B), typeof(C)) then NEST can automagically
			/// create a TypeSelector based on the hits _type property.
			/// </summary>
			public static void CloseOverAutomagicCovariantResultSelector(ElasticInferrer infer, ISearchTemplateRequest self)
			{
				if (infer == null || self == null) return;
				var returnType = self.ClrType;

				if (returnType == null) return;

				var types = (self.Types ?? Enumerable.Empty<TypeName>()).Where(t => t.Type != null).ToList();
				if (self.TypeSelector != null || !types.HasAny(t => t.Type != returnType))
					return;

				var typeDictionary = types.ToDictionary(infer.TypeName, t => t.Type);
				self.TypeSelector = (o, h) =>
				{
					Type t;
					return !typeDictionary.TryGetValue(h.Type, out t) ? returnType : t;
				};
			}
		}
	}
}
