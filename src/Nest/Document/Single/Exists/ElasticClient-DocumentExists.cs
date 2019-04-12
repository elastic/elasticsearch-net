using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using ExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Check if a document exists without returning its contents
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe what document we are looking for</param>
		IExistsResponse DocumentExists<TDocument>(DocumentPath<TDocument> document, Func<DocumentExistsDescriptor<TDocument>, IDocumentExistsRequest> selector = null)
			where TDocument : class;

		/// <inheritdoc />
		IExistsResponse DocumentExists(IDocumentExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> DocumentExistsAsync<TDocument>(DocumentPath<TDocument> document,
			Func<DocumentExistsDescriptor<TDocument>, IDocumentExistsRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class;

		/// <inheritdoc />
		Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse DocumentExists<TDocument>(DocumentPath<TDocument> document, Func<DocumentExistsDescriptor<TDocument>, IDocumentExistsRequest> selector = null)
			where TDocument : class =>
			DocumentExists(selector.InvokeOrDefault(new DocumentExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public IExistsResponse DocumentExists(IDocumentExistsRequest request) =>
			DoRequest<IDocumentExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<DocumentExistsDescriptor<TDocument>, IDocumentExistsRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class =>
			DocumentExistsAsync(selector.InvokeOrDefault(new DocumentExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)), ct);

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDocumentExistsRequest, IExistsResponse, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
