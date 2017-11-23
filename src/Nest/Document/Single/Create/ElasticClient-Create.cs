using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a new typed document in a specific index. If a document with the same index, type and id already exists,
		/// A 409 Conflict HTTP response status code and error will be returned.
		/// </summary>
		/// <typeparam name="TDocument">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">The document to be created. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings"/> for <typeparamref name="TDocument"/></para>
		/// <para>2. <see cref="ElasticsearchTypeAttribute.IdProperty"/> property on <see cref="ElasticsearchTypeAttribute"/> applied to <typeparamref name="TDocument"/></para>
		/// <para>3. A property named Id on <typeparamref name="TDocument"/></para>
		/// </param>
		/// <param name="selector">Optionally further describe the create operation i.e override type/index/id</param>
		ICreateResponse CreateDocument<TDocument>(TDocument document) where TDocument : class;
		ICreateResponse Create<TDocument>(TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector)
			where TDocument : class;

		/// <inheritdoc/>
		ICreateResponse Create<TDocument>(ICreateRequest<TDocument> request) where TDocument : class;

		/// <inheritdoc/>
		Task<ICreateResponse> CreateDocumentAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class;

		Task<ICreateResponse> CreateAsync<TDocument>(
			TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class;

		/// <inheritdoc/>
		Task<ICreateResponse> CreateAsync<TDocument>(ICreateRequest<TDocument> request, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICreateResponse CreateDocument<TDocument>(TDocument document) where TDocument : class => this.Create(document, s => s);

		public ICreateResponse Create<TDocument>(TDocument document, Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector)
			where TDocument :
			class => this.Create(selector?.InvokeOrDefault(new CreateDescriptor<TDocument>(document)));

		/// <inheritdoc/>
		public ICreateResponse Create<TDocument>(ICreateRequest<TDocument> request) where TDocument : class =>
			this.Dispatcher.Dispatch<ICreateRequest<TDocument>, CreateRequestParameters, CreateResponse>(
				request,
				this.LowLevelDispatch.CreateDispatch<CreateResponse, TDocument>
			);

		/// <inheritdoc/>
		public Task<ICreateResponse> CreateDocumentAsync<TDocument>(
			TDocument document,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class =>
			this.CreateAsync(document, s => s, cancellationToken);

		/// <inheritdoc/>
		public Task<ICreateResponse> CreateAsync<TDocument>(
			TDocument document,
			Func<CreateDescriptor<TDocument>, ICreateRequest<TDocument>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class =>
			this.CreateAsync(selector?.InvokeOrDefault(new CreateDescriptor<TDocument>(document)), cancellationToken);

		/// <inheritdoc/>
		public Task<ICreateResponse> CreateAsync<TDocument>(ICreateRequest<TDocument> request, CancellationToken cancellationToken = default(CancellationToken))
			where TDocument : class =>
			this.Dispatcher.DispatchAsync<ICreateRequest<TDocument>, CreateRequestParameters, CreateResponse, ICreateResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.CreateDispatchAsync<CreateResponse, TDocument>
			);
	}
}
