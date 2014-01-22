using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{
		public IGetMappingResponse GetMapping(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			//TODO: this will barf come back and fix the unit test that should fail on this
			//IDictionary<string, RootObjectMapping>
			return this.Dispatch<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatch(p)
			);
		}
		public Task<IGetMappingResponse> GetMappingAsync(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			//TODO: this will barf come back and fix the unit test that should fail on this
			//IDictionary<string, RootObjectMapping>
			return this.DispatchAsync<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse, IGetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatchAsync(p)
			);
		}

	}
}