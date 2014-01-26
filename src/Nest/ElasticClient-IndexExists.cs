using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

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
				allow404:true
			);
		}
		public Task<IIndexExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.DispatchAsync<IndexExistsDescriptor, IndexExistsQueryString, IndexExistsResponse, IIndexExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync(p),
				allow404:true
			);
		}
	}
}
