using System.Threading;
using System.Threading.Tasks;

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

		/// <inheritdoc cref="IElasticClient.IndexDocument{TDocument}" />
		Task<IndexResponse> IndexDocumentAsync<T>(T document, CancellationToken ct = default)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IElasticClient.IndexDocument{TDocument}" />
		public IndexResponse IndexDocument<TDocument>(TDocument document) where TDocument : class => Index(document, s => s);

		/// <inheritdoc cref="IElasticClient.IndexDocument{TDocument}" />
		public Task<IndexResponse> IndexDocumentAsync<TDocument>(TDocument document, CancellationToken ct = default)
			where TDocument : class =>
			IndexAsync(document, s => s, ct);
	}
}
