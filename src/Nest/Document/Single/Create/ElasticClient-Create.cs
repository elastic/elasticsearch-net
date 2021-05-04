// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a new typed document in a specific index. If a document with the same index, type and id already exists,
		/// A 409 Conflict HTTP response status code and error will be returned.
		/// </summary>
		/// <typeparam name="TDocument">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">
		/// The document to be created. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings" /> for <typeparamref name="TDocument" /></para>
		/// <para>
		/// 2. <see cref="ElasticsearchTypeAttribute.IdProperty" /> property on <see cref="ElasticsearchTypeAttribute" /> applied to
		/// <typeparamref name="TDocument" />
		/// </para>
		/// <para>3. A property named Id on <typeparamref name="TDocument" /></para>
		/// </param>
		/// <param name="selector">Optionally further describe the create operation i.e override type/index/id</param>
		CreateResponse CreateDocument<TDocument>(TDocument document) where TDocument : class;

		/// <inheritdoc cref="CreateDocument{TDocument}"/>
		Task<CreateResponse> CreateDocumentAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default)
			where TDocument : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IElasticClient.CreateDocument{TDocument}"/>
		public CreateResponse CreateDocument<TDocument>(TDocument document)
			where TDocument : class => Create(document, null);

		/// <inheritdoc cref="IElasticClient.CreateDocument{TDocument}"/>
		public Task<CreateResponse> CreateDocumentAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default)
			where TDocument : class => CreateAsync(document, null, cancellationToken);
	}
}
