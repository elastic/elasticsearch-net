using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetFieldMappingConverter = Func<IApiCallDetails, Stream, GetFieldMappingResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request);

		/// <inheritdoc/>
		Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetFieldMappingResponse GetFieldMapping<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class => 
			this.GetFieldMapping(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)));

		/// <inheritdoc/>
		public IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest request) => 
			this.Dispatcher.Dispatch<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse>(
				request,
				new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, request, s)),
				(p, d) => this.LowLevelDispatch.IndicesGetFieldMappingDispatch<GetFieldMappingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class =>
			this.GetFieldMappingAsync(selector.InvokeOrDefault(new GetFieldMappingDescriptor<T>(fields)));

		/// <inheritdoc/>
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request) => 
			this.Dispatcher.DispatchAsync<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse, IGetFieldMappingResponse>(
				request,
				new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, request, s)),
				(p, d) => this.LowLevelDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(p)
			);

		//TODO DictionaryResponse!
		private GetFieldMappingResponse DeserializeGetFieldMappingResponse(IApiCallDetails response, IGetFieldMappingRequest d, Stream stream)
		{
			var dict = response.Success
				? Serializer.Deserialize<IndexFieldMappings>(stream)
				: null;
			return new GetFieldMappingResponse(response, dict, this.ConnectionSettings.Inferrer);
		}

	}
}