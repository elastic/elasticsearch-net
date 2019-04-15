using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get mapping operation</param>
		GetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null) where T : class;

		/// <inheritdoc />
		GetMappingResponse GetMapping(IGetMappingRequest request);

		/// <inheritdoc />
		Task<GetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<GetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null)
			where T : class =>
			GetMapping(selector.InvokeOrDefault(new GetMappingDescriptor<T>()));

		/// <inheritdoc />
		public GetMappingResponse GetMapping(IGetMappingRequest request) =>
			DoRequest<IGetMappingRequest, GetMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetMappingResponse> GetMappingAsync<T>(
			Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			GetMappingAsync(selector.InvokeOrDefault(new GetMappingDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<GetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetMappingRequest, GetMappingResponse, GetMappingResponse>(request, request.RequestParameters, ct);
	}
}
