using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The explain api computes a score explanation for a query and a specific document. 
		/// This can give useful feedback whether a document matches or didn’t match a specific query.
		/// <para> </para><a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html</a>
		/// </summary>
		IExplainResponse<T> Explain<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)
			where T : class;

		/// <inheritdoc/>
		IExplainResponse<T> Explain<T>(IExplainRequest<T> request)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document,Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)
			where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)
			where T : class =>
			this.Explain<T>(selector?.Invoke(new ExplainDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(IExplainRequest<T> request)
			where T : class => 
			this.Dispatcher.Dispatch<IExplainRequest<T>, ExplainRequestParameters, ExplainResponse<T>>(
				request,
				this.LowLevelDispatch.ExplainDispatch<ExplainResponse<T>>
			);

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)
			where T : class => 
			this.ExplainAsync<T>(selector?.Invoke(new ExplainDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)
			where T : class => 
			this.Dispatcher.DispatchAsync<IExplainRequest<T>, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				request,
				this.LowLevelDispatch.ExplainDispatchAsync<ExplainResponse<T>>
			);
	}
}