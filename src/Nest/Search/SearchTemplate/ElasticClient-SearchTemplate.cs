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
		ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class;
	}

	public partial class ElasticClient
	{
		public ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			SearchTemplate<T, T>(selector);

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class =>
			SearchTemplate<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()));

		public ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request) where T : class =>
			SearchTemplate<T, T>(request);

		public ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class =>
			Dispatcher.Dispatch<ISearchTemplateRequest, SearchTemplateRequestParameters, SearchResponse<TResult>>(
				request,
				LowLevelDispatch.SearchTemplateDispatch<SearchResponse<TResult>>
			);

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			SearchTemplateAsync<T, T>(selector, cancellationToken);

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class =>
			SearchTemplateAsync<T, TResult>(selector?.Invoke(new SearchTemplateDescriptor<T>()), cancellationToken);

		public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			SearchTemplateAsync<T, T>(request, cancellationToken);

		public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class =>
			Dispatcher.DispatchAsync<ISearchTemplateRequest, SearchTemplateRequestParameters, SearchResponse<TResult>, ISearchResponse<TResult>>(
				request,
				cancellationToken,
				LowLevelDispatch.SearchTemplateDispatchAsync<SearchResponse<TResult>>
			);
	}
}
