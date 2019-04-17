using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		SearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class;

		/// <inheritdoc />
		SearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		SearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc />
		SearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<SearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<SearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken ct = default
		)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<SearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<SearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request,
			CancellationToken ct = default
		)
			where T : class
			where TResult : class;
	}

	public partial class ElasticClient
	{
		public SearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			SearchTemplate<T, T>(selector);

		public SearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class =>
			SearchTemplate<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()));

		public SearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class =>
			SearchTemplate<T, T>(request);

		public SearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class =>
			DoRequest<ISearchTemplateRequest, SearchResponse<TResult>>(request, request.RequestParameters);

		public Task<SearchResponse<T>> SearchTemplateAsync<T>(
			Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			SearchTemplateAsync<T, T>(selector, ct);

		public Task<SearchResponse<TResult>> SearchTemplateAsync<T, TResult>(
			Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken ct = default
		)
			where T : class
			where TResult : class =>
			SearchTemplateAsync<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()), ct);

		public Task<SearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request, CancellationToken ct = default)
			where T : class =>
			SearchTemplateAsync<T, T>(request, ct);

		public Task<SearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request, CancellationToken ct = default)
			where T : class
			where TResult : class =>
			DoRequestAsync<ISearchTemplateRequest, SearchResponse<TResult>>(request, request.RequestParameters, ct);
	}
}
