using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IElasticsearchResponse, Stream, GetIndexResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetIndexResponse GetIndex(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector)
		{
			return this.Dispatcher.Dispatch<GetIndexDescriptor, GetIndexRequestParameters, GetIndexResponse>(
				getIndexSelector,
				(p, d) => this.LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest)
		{
			return this.Dispatcher.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				createIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetDispatch<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector)
		{
			return this.Dispatcher.DispatchAsync<GetIndexDescriptor, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				getIndexSelector,
				(p, d) => this.LowLevelDispatch.IndicesGetDispatchAsync<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				createIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetDispatchAsync<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		private GetIndexResponse DeserializeGetIndexResponse(IElasticsearchResponse response, Stream stream)
		{
			return new GetIndexResponse
			{
				Indices = this.Serializer.Deserialize<Dictionary<string, IndexSettings>>(stream)
			};
		}

	}
}