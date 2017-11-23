using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The explain api computes a score explanation for a query and a specific document.
		/// This can give useful feedback whether a document matches or didn’t match a specific query.
		/// <para> </para><a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html</a>
		/// </summary>
		IExplainResponse<TDocument> Explain<TDocument>(DocumentPath<TDocument> document, Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector)
			where TDocument : class;

		/// <inheritdoc/>
		IExplainResponse<TDocument> Explain<TDocument>(IExplainRequest<TDocument> request)
			where TDocument : class;

		/// <inheritdoc/>
		Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(DocumentPath<TDocument> document,Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class;

		/// <inheritdoc/>
		Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(IExplainRequest<TDocument> request, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExplainResponse<TDocument> Explain<TDocument>(DocumentPath<TDocument> document, Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector)
			where TDocument : class =>
			this.Explain<TDocument>(selector?.Invoke(new ExplainDescriptor<TDocument>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExplainResponse<TDocument> Explain<TDocument>(IExplainRequest<TDocument> request)
			where TDocument : class =>
			this.Dispatcher.Dispatch<IExplainRequest<TDocument>, ExplainRequestParameters, ExplainResponse<TDocument>>(
				request,
				this.LowLevelDispatch.ExplainDispatch<ExplainResponse<TDocument>, TDocument>
			);

		/// <inheritdoc/>
		public Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(DocumentPath<TDocument> document, Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class =>
			this.ExplainAsync<TDocument>(selector?.Invoke(new ExplainDescriptor<TDocument>(document.Self.Index, document.Self.Type, document.Self.Id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(IExplainRequest<TDocument> request, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class =>
			this.Dispatcher.DispatchAsync<IExplainRequest<TDocument>, ExplainRequestParameters, ExplainResponse<TDocument>, IExplainResponse<TDocument>>(
				request,
				cancellationToken,
				this.LowLevelDispatch.ExplainDispatchAsync<ExplainResponse<TDocument>, TDocument>
			);
	}
}
