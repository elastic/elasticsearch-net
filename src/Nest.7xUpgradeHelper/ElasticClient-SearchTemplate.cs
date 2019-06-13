using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The /_search/template endpoint allows to use the mustache language to pre render search
		/// requests, before they are executed and fill existing templates with template parameters.
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		/// <returns></returns>
		public static SearchResponse<T> SearchTemplate<T>(this IElasticClient client,Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class;

		/// <inheritdoc />
		SearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		public static SearchResponse<T> SearchTemplate<T>(this IElasticClient client,ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc />
		SearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		public static Task<SearchResponse<T>> SearchTemplateAsync<T>(this IElasticClient client,Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector,
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
		public static Task<SearchResponse<T>> SearchTemplateAsync<T>(this IElasticClient client,ISearchTemplateRequest request,
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

}
