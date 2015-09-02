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
		IExistsResponse IndexExists(Func<IndexExistsDescriptor, IIndexExistsRequest> selector);

		/// <inheritdoc/>
		IExistsResponse IndexExists(IIndexExistsRequest indexExistsRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IIndexExistsRequest> selector);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexExistsRequest);
	}


	public partial class ElasticClient
	{
		private ExistsResponse DeserializeExistsResponse(IApiCallDetails response, Stream stream) => new ExistsResponse(response);

		/// <inheritdoc/>
		public IExistsResponse IndexExists(Func<IndexExistsDescriptor, IIndexExistsRequest> selector) => 
			this.Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				selector?.Invoke(new IndexExistsDescriptor()),
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public IExistsResponse IndexExists(IIndexExistsRequest indexRequest) => 
			this.Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				indexRequest,
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IIndexExistsRequest> selector) => 
			this.Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector?.Invoke(new IndexExistsDescriptor()),
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexRequest) => 
			this.Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest,
				new IndexExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(p)
			);
	}
}