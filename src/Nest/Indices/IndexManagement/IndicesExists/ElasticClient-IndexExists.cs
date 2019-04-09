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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) =>
			IndexExists(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc />
		public IExistsResponse IndexExists(IIndexExistsRequest request) =>
			Dispatch2<IIndexExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(
			Indices indices,
			Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		) => IndexExistsAsync(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IIndexExistsRequest, IExistsResponse, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
