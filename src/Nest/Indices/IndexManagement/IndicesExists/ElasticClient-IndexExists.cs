using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Used to check if the index (indices) exists or not.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-exists.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the index exist operation</param>
		IExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc />
		IExistsResponse IndexExists(IIndexExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) =>
			IndexExists(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc />
		public IExistsResponse IndexExists(IIndexExistsRequest request) =>
			Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IndexExistsAsync(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(p, c)
			);
	}
}
