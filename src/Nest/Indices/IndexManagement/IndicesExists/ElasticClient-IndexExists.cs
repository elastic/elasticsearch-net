using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse IndexExists(IIndexExistsRequest indexRequest)
		{
			return this.Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				indexRequest,
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatch<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexRequest)
		{
			return this.Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest,
				(p, d) => this.LowLevelDispatch.IndicesExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}


		private ExistsResponse DeserializeExistsResponse(IApiCallDetails response, Stream stream)
		{
			return new ExistsResponse(response);
		}
	}
}