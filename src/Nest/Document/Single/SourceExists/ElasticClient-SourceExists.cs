using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using ExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Check if a document's source exists without returning its contents
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe what document we are looking for</param>
		IExistsResponse SourceExists<T>(DocumentPath<T> document, Func<SourceExistsDescriptor<T>, ISourceExistsRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		IExistsResponse SourceExists(ISourceExistsRequest request);

		/// <inheritdoc/>
		Task<IExistsResponse> SourceExistsAsync<T>(DocumentPath<T> document, Func<SourceExistsDescriptor<T>, ISourceExistsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse SourceExists<T>(DocumentPath<T> document, Func<SourceExistsDescriptor<T>, ISourceExistsRequest> selector = null) where T : class =>
			this.SourceExists(selector.InvokeOrDefault(new SourceExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExistsResponse SourceExists(ISourceExistsRequest request) =>
			this.Dispatcher.Dispatch<ISourceExistsRequest, SourceExistsRequestParameters, ExistsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ExistsSourceDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> SourceExistsAsync<T>(DocumentPath<T> document, Func<SourceExistsDescriptor<T>, ISourceExistsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.SourceExistsAsync(selector.InvokeOrDefault(new SourceExistsDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ISourceExistsRequest, SourceExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.ExistsSourceDispatchAsync<ExistsResponse>(p, c)
			);
	}
}
