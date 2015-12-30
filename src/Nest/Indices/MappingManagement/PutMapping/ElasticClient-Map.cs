using System;
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
		IPutMappingResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IPutMappingResponse Map(IPutMappingRequest request);

		/// <inheritdoc/>
		Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IPutMappingResponse> MapAsync(IPutMappingRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutMappingResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class => 
			this.Map(selector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public IPutMappingResponse Map(IPutMappingRequest request) => 
			this.Dispatcher.Dispatch<IPutMappingRequest, PutMappingRequestParameters, PutMappingResponse>(
				request,
				this.LowLevelDispatch.IndicesPutMappingDispatch<PutMappingResponse>
			);

		/// <inheritdoc/>
		public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class => 
			this.MapAsync(selector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request) => 
			this.Dispatcher.DispatchAsync<IPutMappingRequest, PutMappingRequestParameters, PutMappingResponse, IPutMappingResponse>(
				request,
				this.LowLevelDispatch.IndicesPutMappingDispatchAsync<PutMappingResponse>
			);
	}
}