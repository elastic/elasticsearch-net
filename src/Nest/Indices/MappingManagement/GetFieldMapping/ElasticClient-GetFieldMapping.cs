using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetFieldMappingConverter = Func<IApiCallDetails, Stream, GetFieldMappingResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetFieldMappingResponse GetFieldMapping<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest getFieldMappingRequest);

		/// <inheritdoc/>
		Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest getFieldMappingRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetFieldMappingResponse GetFieldMapping<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class => 
			this.GetFieldMapping(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>()));

		/// <inheritdoc/>
		public IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest getFieldMappingRequest) => 
			this.Dispatcher.Dispatch<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse>(
				getFieldMappingRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetFieldMappingDispatch<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);

		/// <inheritdoc/>
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class =>
			this.GetFieldMappingAsync(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest getFieldMappingRequest) => 
			this.Dispatcher.DispatchAsync<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse, IGetFieldMappingResponse>(
				getFieldMappingRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);
		//TODO DictionaryResponse!
		private GetFieldMappingResponse DeserializeGetFieldMappingResponse(IApiCallDetails response, IGetFieldMappingRequest d, Stream stream)
		{
			var dict = response.Success
				? Serializer.Deserialize<IndexFieldMappings>(stream)
				: null;
			return new GetFieldMappingResponse(response, dict);
		}

	}
}