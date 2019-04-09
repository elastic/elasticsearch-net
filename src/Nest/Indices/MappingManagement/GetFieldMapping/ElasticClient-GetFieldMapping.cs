using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request);

		/// <inheritdoc />
		Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class =>
			GetFieldMapping(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)));

		/// <inheritdoc />
		public IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request) =>
			Dispatch2<IGetFieldMappingRequest, GetFieldMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(
			Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			GetFieldMappingAsync(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)), ct);

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetFieldMappingRequest, IGetFieldMappingResponse, GetFieldMappingResponse>(request, request.RequestParameters, ct);
	}
}
