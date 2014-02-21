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

		public IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector) where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var descriptor = mappingSelector(new PutMappingDescriptor<T>(this._connectionSettings));
			return this.Dispatch<PutMappingDescriptor<T>, PutMappingQueryString, IndicesResponse>(
				descriptor,
				(p, d) =>
				{
					var o = new Dictionary<string, RootObjectMapping>
					{
						{p.Type, d._Mapping}
					};
					return this.RawDispatch.IndicesPutMappingDispatch(p, o);
				}
			);
		}	
		public Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector) where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var descriptor = mappingSelector(new PutMappingDescriptor<T>(this._connectionSettings));
			return this.DispatchAsync<PutMappingDescriptor<T>, PutMappingQueryString, IndicesResponse, IIndicesResponse>(
				descriptor,
				(p, d) =>
				{
					var o = new Dictionary<string, RootObjectMapping>
					{
						{p.Type, d._Mapping}
					};
					return this.RawDispatch.IndicesPutMappingDispatchAsync(p, o);
				}
			);
		}
	}
}