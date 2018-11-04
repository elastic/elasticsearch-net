using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetFieldMappingConverter = Func<IApiCallDetails, Stream, GetFieldMappingResponse>;

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
				new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, request, s)),
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
				new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, request, s)),
				(p, d, c) => LowLevelDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(p, c)
			);

		//TODO DictionaryResponse!
		private GetFieldMappingResponse DeserializeGetFieldMappingResponse(IApiCallDetails response, IGetFieldMappingRequest d, Stream stream)
		{
			var dict = response.Success
				? Serializer.Deserialize<Dictionary<string, TypeFieldMappings>>(stream)
				: null;
			return new GetFieldMappingResponse(response, dict, ConnectionSettings.Inferrer);
		}
	}
}
