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
		/// <inheritdoc/>
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class =>
			this.Search<T, T>(searchSelector);

		/// <inheritdoc/>
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class
			where TResult : class =>
			this.Dispatcher.Dispatch<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>>(
				searchSelector?.InvokeOrDefault(new SearchDescriptor<T>()),
				(p, d) => this.LowLevelDispatch.SearchDispatch<SearchResponse<TResult>>(
					this.AttachCustomConverterWhenNeeded<T, TResult>(p, d), d
					)
				);

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
					this.AttachCustomConverterWhenNeeded<T, TResult>(p, d), d
					)
				);

		/// <inheritdoc/>
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class => 
			this.SearchAsync<T, T>(searchSelector);

		/// <inheritdoc/>
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
			where T : class
			where TResult : class =>
			this.Dispatcher.DispatchAsync<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>, ISearchResponse<TResult>>(
				searchSelector?.InvokeOrDefault(new SearchDescriptor<T>()),
				(p, d) => this.LowLevelDispatch.SearchDispatchAsync<SearchResponse<TResult>>(
					this.AttachCustomConverterWhenNeeded<T, TResult>(p, d), d
					)
				);

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
					this.AttachCustomConverterWhenNeeded<T, TResult>(p, d), d
					)
				);

		private ElasticsearchPathInfo<SearchRequestParameters> AttachCustomConverterWhenNeeded<T, TResult>(ElasticsearchPathInfo<SearchRequestParameters> p, ISearchRequest d)
			where T : class
			where TResult : class => 
			p.DeserializationState(this.CreateSearchDeserializer<T, TResult>(d));

		private Func<IApiCallDetails, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class
		{
			SearchPathInfo.CloseOverAutomagicCovariantResultSelector(this.Infer, request);
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