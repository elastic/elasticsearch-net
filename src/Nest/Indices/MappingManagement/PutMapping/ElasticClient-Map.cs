using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{

		/// <summary>
		/// The put mapping API allows to register specific mapping definition for a specific type.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-put-mapping.html
		/// </summary>
		/// <typeparam name="T">The type we want to map in elasticsearch</typeparam>
		/// <param name="mappingSelector">A descriptor to describe the mapping of our type</param>
		IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> mappingSelector)
			where T : class;

		/// <inheritdoc/>
		IIndicesResponse Map(IPutMappingRequest putMappingRequest);

		/// <inheritdoc/>
		Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> mappingSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IIndicesResponse> MapAsync(IPutMappingRequest putMappingRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> mappingSelector)
			where T : class => 
			this.Map(mappingSelector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public IIndicesResponse Map(IPutMappingRequest putMappingRequest) => 
			this.Dispatcher.Dispatch<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse>(
				putMappingRequest,
				this.LowLevelDispatch.IndicesPutMappingDispatch<IndicesResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> mappingSelector)
			where T : class => 
			this.MapAsync(mappingSelector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IIndicesResponse> MapAsync(IPutMappingRequest putMappingRequest) => 
			this.Dispatcher.DispatchAsync<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse, IIndicesResponse>(
				putMappingRequest,
				this.LowLevelDispatch.IndicesPutMappingDispatchAsync<IndicesResponse>
			);
	}
}