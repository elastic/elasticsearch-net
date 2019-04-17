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
		/// Check if a document's source exists without returning its contents
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe what document we are looking for</param>
		ExistsResponse SourceExists<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null)
			where TDocument : class;

		/// <inheritdoc />
		ExistsResponse SourceExists(ISourceExistsRequest request);

		/// <inheritdoc />
		Task<ExistsResponse> SourceExistsAsync<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class;

		/// <inheritdoc />
		Task<ExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse SourceExists<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null)
			where TDocument : class =>
			SourceExists(selector.InvokeOrDefault(new SourceExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public ExistsResponse SourceExists(ISourceExistsRequest request) =>
			DoRequest<ISourceExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> SourceExistsAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class =>
			SourceExistsAsync(selector.InvokeOrDefault(new SourceExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)), ct);

		/// <inheritdoc />
		public Task<ExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISourceExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
