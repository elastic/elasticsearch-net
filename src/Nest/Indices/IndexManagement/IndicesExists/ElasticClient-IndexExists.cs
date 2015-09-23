using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Used to check if the index (indices) exists or not. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-exists.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the index exist operation</param>
		IExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc/>
		IExistsResponse IndexExists(IIndexExistsRequest indexExistsRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexExistsRequest);
	}


	public partial class ElasticClient
	{
		private ExistsResponse DeserializeExistsResponse(IApiCallDetails response, Stream stream) => new ExistsResponse(response);

		/// <inheritdoc/>
		public IExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) =>
			this.IndexExists(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc/>
		public IExistsResponse IndexExists(IIndexExistsRequest indexRequest) => 
			this.Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				indexRequest,
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) => 
			this.IndexExistsAsync(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexRequest) => 
			this.Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest,
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(p)
			);
	}
}