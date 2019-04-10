using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The explain api computes a score explanation for a query and a specific document.
		/// This can give useful feedback whether a document matches or didn’t match a specific query.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html</a>
		/// </summary>
		IExplainResponse<TDocument> Explain<TDocument>(DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector
		)
			where TDocument : class;

		/// <inheritdoc />
		IExplainResponse<TDocument> Explain<TDocument>(IExplainRequest<TDocument> request)
			where TDocument : class;

		/// <inheritdoc />
		Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector, CancellationToken ct = default
		)
			where TDocument : class;

		/// <inheritdoc />
		Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(IExplainRequest<TDocument> request,
			CancellationToken ct = default
		)
			where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExplainResponse<TDocument> Explain<TDocument>(DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector
		)
			where TDocument : class =>
			Explain(selector?.Invoke(new ExplainDescriptor<TDocument>(
				document.Document, document.Self.Index, document.Self.Id
			)));

		/// <inheritdoc />
		public IExplainResponse<TDocument> Explain<TDocument>(IExplainRequest<TDocument> request)
			where TDocument : class =>
			DoRequest<IExplainRequest<TDocument>, ExplainResponse<TDocument>>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector,
			CancellationToken ct = default
		)
			where TDocument : class =>
			ExplainAsync(selector?.Invoke(new ExplainDescriptor<TDocument>(
				document.Document, document.Self.Index, document.Self.Id
			)), ct);

		/// <inheritdoc />
		public Task<IExplainResponse<TDocument>> ExplainAsync<TDocument>(IExplainRequest<TDocument> request, CancellationToken ct = default)
			where TDocument : class =>
			DoRequestAsync<IExplainRequest<TDocument>, IExplainResponse<TDocument>, ExplainResponse<TDocument>>(request, request.RequestParameters, ct);
	}
}
