using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetMappingConverter = Func<IElasticsearchResponse, Stream, GetMappingResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetMappingResponse GetMapping(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			return this.Dispatch<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatch<GetMappingResponse>(
					p,
					new GetMappingConverter((r, s) => DeserializeGetMappingResponse(r, d, s))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetMappingResponse> GetMappingAsync(Func<GetMappingDescriptor, GetMappingDescriptor> selector)
		{
			return this.DispatchAsync<GetMappingDescriptor, GetMappingQueryString, GetMappingResponse, IGetMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetMappingDispatchAsync<GetMappingResponse>(
					p,
					new GetMappingConverter((r, s) => DeserializeGetMappingResponse(r, d, s))
				)
			);
		}

		private GetMappingResponse DeserializeGetMappingResponse(IElasticsearchResponse response, GetMappingDescriptor d,
			Stream stream)
		{
			var dict = response.Success
				? Serializer.DeserializeInternal<GetRootObjectMappingWrapping>(stream)
				: null;
			return new GetMappingResponse(response, dict);
		}
	}
}