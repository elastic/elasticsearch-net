using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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
		Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IPutMappingResponse> MapAsync(IPutMappingRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
		public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.MapAsync(selector?.Invoke(new PutMappingDescriptor<T>()), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutMappingRequest, PutMappingRequestParameters, PutMappingResponse, IPutMappingResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IndicesPutMappingDispatchAsync<PutMappingResponse>
			);
	}
}
