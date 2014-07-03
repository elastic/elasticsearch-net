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
		public IIndicesResponse DeleteMapping<T>(Func<DeleteMappingDescriptor<T>, DeleteMappingDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteMappingDescriptor<T>, DeleteMappingRequestParameters, IndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteMappingDispatch<IndicesResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> DeleteMappingAsync<T>(Func<DeleteMappingDescriptor<T>, DeleteMappingDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteMappingDescriptor<T>, DeleteMappingRequestParameters, IndicesResponse, IIndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteMappingDispatchAsync<IndicesResponse>(p)
			);
		}
	}
}