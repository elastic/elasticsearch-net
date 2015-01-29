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

			var status = this.RawDispatch.SearchTemplateDispatch<SearchResponse<TResult>>(pathInfo, descriptor);
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

			var status = this.RawDispatch.SearchTemplateDispatch<SearchResponse<TResult>>(pathInfo, request);
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

			return this.RawDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>(pathInfo, descriptor)
				.ContinueWith<ISearchResponse<TResult>>(t =>
				{
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;

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

			return this.RawDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>(pathInfo, request)
				.ContinueWith<ISearchResponse<TResult>>(t =>
				{
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;

					return t.Result.Success
						? t.Result.Response
						: CreateInvalidInstance<SearchResponse<TResult>>(t.Result);
				});
		}

		public IGetSearchTemplateResponse GetSearchTemplate(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<GetSearchTemplateDescriptor, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);
		}

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request)
		{
			return this.Dispatch<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);
		}

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetSearchTemplateDescriptor, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
		}

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)
		{
			return this.DispatchAsync<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
		}

		public IPutSearchTemplateResponse PutSearchTemplate(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<PutSearchTemplateDescriptor, PutTemplateRequestParameters, PutSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.PutTemplateDispatch<PutSearchTemplateResponse>(p, d)
			);
		}

		public IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request)
		{
			return this.Dispatch<IPutSearchTemplateRequest, PutTemplateRequestParameters, PutSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.PutTemplateDispatch<PutSearchTemplateResponse>(p, d)
			);
		}

		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<PutSearchTemplateDescriptor, PutTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>(p, d)
			);
		}

		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)
		{
			return this.DispatchAsync<IPutSearchTemplateRequest, PutTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>(p, d)
			);
		}

		public IDeleteSearchTemplateResponse DeleteSearchTemplate(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);
		}

		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request)
		{
			return this.Dispatch<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);
		}

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
		}

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)
		{
			return this.DispatchAsync<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
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
