using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using GetMappingConverter = Func<IApiCallDetails, Stream, GetMappingResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get mapping operation</param>
		IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IGetMappingResponse GetMapping(IGetMappingRequest request);

		/// <inheritdoc/>
		Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null)
			where T : class =>
			this.GetMapping(selector.InvokeOrDefault(new GetMappingDescriptor<T>()));

		/// <inheritdoc/>
		public IGetMappingResponse GetMapping(IGetMappingRequest request) =>
			this.Dispatcher.Dispatch<IGetMappingRequest, GetMappingRequestParameters, GetMappingResponse>(
				request,
				new GetMappingConverter((r, s) => DeserializeGetMappingResponse(r, request, s)),
				(p, d) => this.LowLevelDispatch.IndicesGetMappingDispatch<GetMappingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.GetMappingAsync(selector.InvokeOrDefault(new GetMappingDescriptor<T>()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetMappingRequest, GetMappingRequestParameters, GetMappingResponse, IGetMappingResponse>(
				request,
				cancellationToken,
				new GetMappingConverter((r, s) => DeserializeGetMappingResponse(r, request, s)),
				(p, d, c) => this.LowLevelDispatch.IndicesGetMappingDispatchAsync<GetMappingResponse>(p, c)
			);

		//TODO this is too geared towards getting a single mapping
		private GetMappingResponse DeserializeGetMappingResponse(IApiCallDetails response, IGetMappingRequest d, Stream stream)
		{
			var dict = response.Success
				? Serializer.Deserialize<GetRootObjectMappingWrapping>(stream)
				: null;
			return new GetMappingResponse(response, dict);
		}

	}
}
