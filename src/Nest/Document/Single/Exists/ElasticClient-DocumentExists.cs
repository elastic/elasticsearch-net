using System;
using System.Collections.Generic;
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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="existsSelector">Describe what document we are looking for</param>
		IExistsResponse DocumentExists<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> existsSelector = null)
			where T : class;

		/// <inheritdoc/>
		IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> existsSelector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest);
	}

	//TODO assume 404 is allowed on head requests I removed this code:
	// d => existsSelector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse DocumentExists<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> existsSelector = null) where T : class =>
			this.DocumentExists(existsSelector.InvokeOrDefault(new DocumentExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest) => 
			this.Dispatcher.Dispatch<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse>(
				documentExistsRequest,
				new ExistConverter(this.DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.ExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> existsSelector = null) where T : class =>
			this.DocumentExistsAsync(existsSelector.InvokeOrDefault(new DocumentExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest) => 
			this.Dispatcher.DispatchAsync<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				documentExistsRequest,
				new ExistConverter(this.DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.ExistsDispatchAsync<ExistsResponse>(p)
			);
	}
}