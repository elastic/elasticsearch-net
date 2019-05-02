using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		GetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request);

		/// <inheritdoc />
		Task<GetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<GetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class =>
			GetFieldMapping(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)));

		/// <inheritdoc />
		public GetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request) =>
			DoRequest<IGetFieldMappingRequest, GetFieldMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetFieldMappingResponse> GetFieldMappingAsync<T>(
			Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			GetFieldMappingAsync(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)), ct);

		/// <inheritdoc />
		public Task<GetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetFieldMappingRequest, GetFieldMappingResponse>(request, request.RequestParameters, ct);
	}
}
