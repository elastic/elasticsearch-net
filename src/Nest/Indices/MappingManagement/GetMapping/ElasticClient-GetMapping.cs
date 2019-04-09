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
		IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null) where T : class;

		/// <inheritdoc />
		IGetMappingResponse GetMapping(IGetMappingRequest request);

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null)
			where T : class =>
			GetMapping(selector.InvokeOrDefault(new GetMappingDescriptor<T>()));

		/// <inheritdoc />
		public IGetMappingResponse GetMapping(IGetMappingRequest request) =>
			Dispatch2<IGetMappingRequest, GetMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetMappingResponse> GetMappingAsync<T>(
			Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			GetMappingAsync(selector.InvokeOrDefault(new GetMappingDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetMappingRequest, IGetMappingResponse, GetMappingResponse>(request, request.RequestParameters, ct);
	}
}
