using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{
		public IGetMappingResponse GetMapping(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			return this.Dispatch<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatch(p),
				(c, d) => this.Serializer.DeserializeGetMappingResponse(c)
			);
		}


		public Task<IGetMappingResponse> GetMappingAsync(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			return this.DispatchAsync<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse, IGetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatchAsync(p),
				(c, d) => this.Serializer.DeserializeGetMappingResponse(c)
			);
		
		}

	}
}