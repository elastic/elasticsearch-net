using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using ExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Check if a document exists without returning its contents
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe what document we are looking for</param>
		IExistsResponse DocumentExists<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		IExistsResponse DocumentExists(IDocumentExistsRequest request);

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse DocumentExists<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector = null) where T : class =>
			this.DocumentExists(selector.InvokeOrDefault(new DocumentExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExistsResponse DocumentExists(IDocumentExistsRequest request) => 
			this.Dispatcher.Dispatch<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse>(
				request,
				new ExistConverter(this.DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.ExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector = null) where T : class =>
			this.DocumentExistsAsync(selector.InvokeOrDefault(new DocumentExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request) => 
			this.Dispatcher.DispatchAsync<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				new ExistConverter(this.DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.ExistsDispatchAsync<ExistsResponse>(p)
			);
	}
}