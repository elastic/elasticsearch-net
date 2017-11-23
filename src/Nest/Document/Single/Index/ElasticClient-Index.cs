using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds or updates a typed JSON document in a specific index, making it searchable.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html</a>
		/// </summary>
		/// <typeparam name="T">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">The document to be indexed. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings"/> for <typeparamref name="T"/></para>
		/// <para>2. <see cref="ElasticsearchTypeAttribute.IdProperty"/> property on <see cref="ElasticsearchTypeAttribute"/> applied to <typeparamref name="T"/></para>
		/// <para>3. A property named Id on <typeparamref name="T"/></para>
		/// </param>
		/// <param name="selector">Optionally further describe the index operation i.e override type, index, id</param>
		IIndexResponse IndexDocument<T>(T document) where T : class;
		IIndexResponse Index<T>(T document, Func<IndexDescriptor<T>, IIndexRequest<T>> selector) where T : class;

		/// <inheritdoc/>
		IIndexResponse Index<T>(IIndexRequest<T> request) where T : class;

		Task<IIndexResponse> IndexDocumentAsync<T>(T document, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(
			T document,
			Func<IndexDescriptor<T>, IIndexRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexResponse IndexDocument<T>(T document) where T : class => this.Index(document, s => s);

		/// <inheritdoc/>
		public IIndexResponse Index<T>(T document, Func<IndexDescriptor<T>, IIndexRequest<T>> selector)
			where T : class =>
			this.Index(selector?.InvokeOrDefault(new IndexDescriptor<T>(document)));

		/// <inheritdoc/>
		public IIndexResponse Index<T>(IIndexRequest<T> request) where T : class=>
			this.Dispatcher.Dispatch<IIndexRequest<T>, IndexRequestParameters, IndexResponse>(
				request,
				this.LowLevelDispatch.IndexDispatch<IndexResponse, T>
			);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexDocumentAsync<T>(T document, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.IndexAsync(document, s => s, cancellationToken);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(T document, Func<IndexDescriptor<T>, IIndexRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.IndexAsync(selector?.InvokeOrDefault(new IndexDescriptor<T>(document)), cancellationToken);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.Dispatcher.DispatchAsync<IIndexRequest<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				request, cancellationToken, this.LowLevelDispatch.IndexDispatchAsync<IndexResponse, T>
			);
	}
}
