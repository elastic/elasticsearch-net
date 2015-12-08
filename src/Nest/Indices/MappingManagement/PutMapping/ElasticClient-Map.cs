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
		/// <param name="selector">A descriptor to describe the mapping of our type</param>
		IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IIndicesResponse Map(IPutMappingRequest request);

		/// <inheritdoc/>
		Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IIndicesResponse> MapAsync(IPutMappingRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class => 
			this.Map(selector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public IIndicesResponse Map(IPutMappingRequest request) => 
			this.Dispatcher.Dispatch<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse>(
				request,
				this.LowLevelDispatch.IndicesPutMappingDispatch<IndicesResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class => 
			this.MapAsync(selector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IIndicesResponse> MapAsync(IPutMappingRequest request) => 
			this.Dispatcher.DispatchAsync<IPutMappingRequest, PutMappingRequestParameters, IndicesResponse, IIndicesResponse>(
				request,
				this.LowLevelDispatch.IndicesPutMappingDispatchAsync<IndicesResponse>
			);
	}
}