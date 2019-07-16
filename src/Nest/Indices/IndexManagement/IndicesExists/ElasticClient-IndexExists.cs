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
		ExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc />
		ExistsResponse IndexExists(IIndexExistsRequest request);

		/// <inheritdoc />
		Task<ExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) =>
			IndexExists(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc />
		public ExistsResponse IndexExists(IIndexExistsRequest request) =>
			DoRequest<IIndexExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexExistsAsync(
			Indices indices,
			Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		) => IndexExistsAsync(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IIndexExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
