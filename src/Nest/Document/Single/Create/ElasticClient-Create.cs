using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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

		CreateResponse Create<TDocument>(TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector)
			where TDocument : class;

		/// <inheritdoc />
		CreateResponse Create<TDocument>(ICreateRequest<TDocument> request) where TDocument : class;

		/// <inheritdoc />
		Task<CreateResponse> CreateDocumentAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default)
			where TDocument : class;

		Task<CreateResponse> CreateAsync<TDocument>(
			TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		Task<CreateResponse> CreateAsync<TDocument>(ICreateRequest<TDocument> request,
			CancellationToken ct = default
		)
			where TDocument : class;
	}

	public partial class ElasticClient
	{
		public CreateResponse Create<TDocument>(TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector)
			where TDocument :
			class => Create(selector?.InvokeOrDefault(new CreateDescriptor<TDocument>(document)));

		/// <inheritdoc />
		public CreateResponse Create<TDocument>(ICreateRequest<TDocument> request) where TDocument : class =>
			DoRequest<ICreateRequest<TDocument>, CreateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CreateResponse> CreateAsync<TDocument>(
			TDocument document,
			Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector,
			CancellationToken ct = default
		) where TDocument : class =>
			CreateAsync(selector?.InvokeOrDefault(new CreateDescriptor<TDocument>(document)), ct);

		/// <inheritdoc />
		public Task<CreateResponse> CreateAsync<TDocument>(ICreateRequest<TDocument> request, CancellationToken ct = default)
			where TDocument : class =>
			DoRequestAsync<ICreateRequest<TDocument>, CreateResponse, CreateResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public CreateResponse CreateDocument<TDocument>(TDocument document) where TDocument : class => Create(document, s => s);

		/// <inheritdoc />
		public Task<CreateResponse> CreateDocumentAsync<TDocument>(
			TDocument document,
			CancellationToken cancellationToken = default
		) where TDocument : class =>
			CreateAsync(document, s => s, cancellationToken);
	}
}
