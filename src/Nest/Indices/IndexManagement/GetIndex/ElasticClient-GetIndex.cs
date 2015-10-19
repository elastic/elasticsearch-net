using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> getIndexSelector = null);

		/// <inheritdoc/>
		IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> getIndexSelector = null);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> getIndexSelector = null) =>
			this.GetIndex(getIndexSelector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest) => 
			this.Dispatcher.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				createIndexRequest,
				new GetIndexResponseConverter(this.DeserializeGetIndexResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> getIndexSelector = null) =>
			this.GetIndexAsync(getIndexSelector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest) => 
			this.Dispatcher.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				createIndexRequest,
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