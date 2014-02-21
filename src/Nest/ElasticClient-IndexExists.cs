using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Check if the index already exists
		/// </summary>
		public IIndexExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatch<IndexExistsDescriptor, IndexExistsQueryString, IndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatch(p),
				(c, d) => new IndexExistsResponse(c),
				allow404:true
			);
		}
		public Task<IIndexExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.DispatchAsync<IndexExistsDescriptor, IndexExistsQueryString, IndexExistsResponse, IIndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync(p),
				(c, d) => new IndexExistsResponse(c),
				allow404:true
			);
		}
	}
}
