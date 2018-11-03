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
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
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
			Dispatcher.Dispatch<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesGetFieldMappingDispatch<GetFieldMappingResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			GetFieldMappingAsync(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse, IGetFieldMappingResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(p, c)
			);
	}
}
