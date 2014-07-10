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
		public IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector) 
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var descriptor = mappingSelector(new PutMappingDescriptor<T>(_connectionSettings));
			return this.Map(descriptor);
		}

		/// <inheritdoc />
		public IIndicesResponse Map(IPutMappingRequest putMappingRequest) 
		{
			return this.Dispatch<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse>(
				putMappingRequest,
				(p, d) =>
				{
					var o = new Dictionary<string, RootObjectMapping>
					{
						{p.Type, d.Mapping}
					};
					return this.RawDispatch.IndicesPutMappingDispatch<IndicesResponse>(p, o);
				}
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var descriptor = mappingSelector(new PutMappingDescriptor<T>(_connectionSettings));
			return this.MapAsync(descriptor);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> MapAsync(IPutMappingRequest putMappingRequest)
		{
			return this.DispatchAsync<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse, IIndicesResponse>(
				putMappingRequest,
				(p, d) =>
				{
					var o = new Dictionary<string, RootObjectMapping>
					{
						{p.Type, d.Mapping}
					};
					return this.RawDispatch.IndicesPutMappingDispatchAsync<IndicesResponse>(p, o);
				}
			);
		}

	}
}