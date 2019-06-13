using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
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
		public static CreateResponse CreateDocument<TDocument>(this IElasticClient client,TDocument document) where TDocument : class;

		public static CreateResponse Create<TDocument>(this IElasticClient client,TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector)
			where TDocument : class;

		/// <inheritdoc />
		public static CreateResponse Create<TDocument>(this IElasticClient client,ICreateRequest<TDocument> request) where TDocument : class;

		/// <inheritdoc />
		public static Task<CreateResponse> CreateDocumentAsync<TDocument>(this IElasticClient client,TDocument document, CancellationToken ct = default)
			where TDocument : class;

		public static Task<CreateResponse> CreateAsync<TDocument>(this IElasticClient client,
			TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		public static Task<CreateResponse> CreateAsync<TDocument>(this IElasticClient client,ICreateRequest<TDocument> request,
			CancellationToken ct = default
		)
			where TDocument : class;
	}

}
