using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc/>
		IGetIndexResponse GetIndex(IGetIndexRequest request);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			this.GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(IGetIndexRequest request) => 
			this.Dispatcher.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				request,
				new GetIndexResponseConverter(this.DeserializeGetIndexResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			this.GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request) => 
			this.Dispatcher.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				request,
				new GetIndexResponseConverter(this.DeserializeGetIndexResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetDispatchAsync<GetIndexResponse>(p)
			);

		//TODO DictionaryResponse
		private GetIndexResponse DeserializeGetIndexResponse(IApiCallDetails response, Stream stream)
		{
			return new GetIndexResponse
			{
				Indices = this.Serializer.Deserialize<Dictionary<string, IndexState>>(stream)
			};
		}

	}
}