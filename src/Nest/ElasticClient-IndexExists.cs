using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IElasticsearchResponse, Stream, IndexExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndexExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatch<IndexExistsDescriptor, IndexExistsRequestParameters, IndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatch<IndexExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				),
				allow404: true
			);
		}

		/// <inheritdoc />
		public Task<IIndexExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.DispatchAsync<IndexExistsDescriptor, IndexExistsRequestParameters, IndexExistsResponse, IIndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync<IndexExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				), 
				allow404: true
			);
		}

		private IndexExistsResponse DeserializeExistsResponse(IElasticsearchResponse response, Stream stream)
		{
			return new IndexExistsResponse(response);
		}
	}
}