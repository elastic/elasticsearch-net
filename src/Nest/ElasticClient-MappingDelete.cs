using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesResponse DeleteMapping(Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector)
		{
			return this.Dispatch<DeleteMappingDescriptor, DeleteMappingRequestParameters, IndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteMappingDispatch<IndicesResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> DeleteMappingAsync(Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector)
		{
			return this.DispatchAsync<DeleteMappingDescriptor, DeleteMappingRequestParameters, IndicesResponse, IIndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteMappingDispatchAsync<IndicesResponse>(p)
			);
		}
	}
}