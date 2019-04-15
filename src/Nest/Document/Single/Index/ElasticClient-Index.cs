using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds or updates a typed JSON document in a specific index, making it searchable.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">
		/// The document to be indexed. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings" /> for <typeparamref name="TDocument" /></para>
		/// <para>
		/// 2. <see cref="ElasticsearchTypeAttribute.IdProperty" /> property on <see cref="ElasticsearchTypeAttribute" /> applied to
		/// <typeparamref name="TDocument" />
		/// </para>
		/// <para>3. A property named Id on <typeparamref name="TDocument" /></para>
		/// </param>
		/// <param name="selector">Optionally further describe the index operation i.e override type, index, id</param>
		IndexResponse IndexDocument<TDocument>(TDocument document) where TDocument : class;

		IndexResponse Index<TDocument>(TDocument document, Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector) where TDocument : class;

		/// <inheritdoc />
		IndexResponse Index<TDocument>(IIndexRequest<TDocument> request) where TDocument : class;

		Task<IndexResponse> IndexDocumentAsync<T>(T document, CancellationToken ct = default)
			where T : class;

		/// <inheritdoc />
		Task<IndexResponse> IndexAsync<TDocument>(
			TDocument document,
			Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector,
			CancellationToken cancellationToken = default
		) where TDocument : class;

		/// <inheritdoc />
		Task<IndexResponse> IndexAsync<TDocument>(IIndexRequest<TDocument> request, CancellationToken ct = default)
			where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IndexResponse Index<TDocument>(TDocument document, Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector)
			where TDocument : class =>
			Index(selector?.InvokeOrDefault(new IndexDescriptor<TDocument>(document)));

		/// <inheritdoc />
		public IndexResponse Index<TDocument>(IIndexRequest<TDocument> request) where TDocument : class =>
			DoRequest<IIndexRequest<TDocument>, IndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IndexResponse> IndexAsync<TDocument>(
			TDocument document,
			Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector,
			CancellationToken cancellationToken = default
		)
			where TDocument : class =>
			IndexAsync(selector?.InvokeOrDefault(new IndexDescriptor<TDocument>(document)), cancellationToken);

		/// <inheritdoc />
		public Task<IndexResponse> IndexAsync<TDocument>(IIndexRequest<TDocument> request, CancellationToken ct = default)
			where TDocument : class =>
			DoRequestAsync<IIndexRequest<TDocument>, IndexResponse, IndexResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public IndexResponse IndexDocument<TDocument>(TDocument document) where TDocument : class => Index(document, s => s);

		/// <inheritdoc />
		public Task<IndexResponse> IndexDocumentAsync<TDocument>(TDocument document, CancellationToken ct = default)
			where TDocument : class =>
			IndexAsync(document, s => s, ct);
	}
}
