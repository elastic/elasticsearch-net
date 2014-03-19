using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IElasticsearchResponse, Stream, IndexExistsResponse>;
	public partial class ElasticClient
	{
		/// <summary>
		/// Check if the index already exists
		/// </summary>
		public IIndexExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatch<IndexExistsDescriptor, IndexExistsQueryString, IndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatch<IndexExistsResponse>(
					p,
					new IndexExistConverter(this.DeserializeExistsResponse)
				)
			);
		}
		public Task<IIndexExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.DispatchAsync<IndexExistsDescriptor, IndexExistsQueryString, IndexExistsResponse, IIndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync<IndexExistsResponse>(
					p,
					new IndexExistConverter(this.DeserializeExistsResponse)
				)
			);
		}
		
		private IndexExistsResponse DeserializeExistsResponse(IElasticsearchResponse response, Stream stream)
		{
			return new IndexExistsResponse(response);
		}
	}
}
