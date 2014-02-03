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
			return this.Dispatch<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatch(p),
				(d, s) => GetMappingResponse(s)
			);
		}


		public Task<IGetMappingResponse> GetMappingAsync(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			return this.DispatchAsync<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse, IGetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatchAsync(p),
				(d, s) => GetMappingResponse(s)
			);
		
		}

		private static GetMappingResponse GetMappingResponse(ConnectionStatus s)
		{
				var dict = s.Success
					? TryDeserializeMapping(s)
					: null;
				return new GetMappingResponse(s, dict);
			
		}

		private static Dictionary<string, RootObjectMapping> TryDeserializeMapping(ConnectionStatus s)
		{
			return s.Deserialize<Dictionary<string, RootObjectMapping>>();
		}
	}
}