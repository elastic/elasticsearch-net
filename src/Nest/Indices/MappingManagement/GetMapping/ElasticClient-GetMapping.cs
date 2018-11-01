using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     The get mapping API allows to retrieve mapping definitions for an index or index/type.
		///     <para> </para>
		///     http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get mapping operation</param>
		IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null) where T : class;

		/// <inheritdoc />
		IGetMappingResponse GetMapping(IGetMappingRequest request);

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null)
			where T : class =>
			GetMapping(selector.InvokeOrDefault(new GetMappingDescriptor<T>()));

		/// <inheritdoc />
		public IGetMappingResponse GetMapping(IGetMappingRequest request) =>
			Dispatcher.Dispatch<IGetMappingRequest, GetMappingRequestParameters, GetMappingResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesGetMappingDispatch<GetMappingResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			GetMappingAsync(selector.InvokeOrDefault(new GetMappingDescriptor<T>()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetMappingRequest, GetMappingRequestParameters, GetMappingResponse, IGetMappingResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesGetMappingDispatchAsync<GetMappingResponse>(p, c)
			);
	}
}
