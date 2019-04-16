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
		IExistsResponse SourceExists<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null)
			where TDocument : class;

		/// <inheritdoc />
		IExistsResponse SourceExists(ISourceExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> SourceExistsAsync<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TDocument : class;

		/// <inheritdoc />
		Task<IExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse SourceExists<TDocument>(DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null)
			where TDocument : class =>
			SourceExists(selector.InvokeOrDefault(new SourceExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public IExistsResponse SourceExists(ISourceExistsRequest request) =>
			Dispatcher.Dispatch<ISourceExistsRequest, SourceExistsRequestParameters, ExistsResponse>(
				request,
				(p, d) => LowLevelDispatch.ExistsSourceDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IExistsResponse> SourceExistsAsync<TDocument>(DocumentPath<TDocument> document,
			Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class =>
			SourceExistsAsync(selector.InvokeOrDefault(new SourceExistsDescriptor<TDocument>(document.Self.Index, document.Self.Id)),
				cancellationToken);

		/// <inheritdoc />
		public Task<IExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ISourceExistsRequest, SourceExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.ExistsSourceDispatchAsync<ExistsResponse>(p, c)
			);
	}
}
