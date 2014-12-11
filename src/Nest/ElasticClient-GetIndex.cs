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
			return this.Dispatch<GetIndexDescriptor, GetIndexRequestParameters, GetIndexResponse>(
				getIndexSelector,
				(p, d) => this.RawDispatch.IndicesGetDispatch<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest)
		{
			return this.Dispatch<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse>(
				createIndexRequest,
				(p, d) => this.RawDispatch.IndicesGetDispatch<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector)
		{
			return this.DispatchAsync<GetIndexDescriptor, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				getIndexSelector,
				(p, d) => this.RawDispatch.IndicesGetDispatchAsync<GetIndexResponse>(
					p.DeserializationState(new GetIndexResponseConverter(this.DeserializeGetIndexResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest)
		{
			return this.DispatchAsync<IGetIndexRequest, GetIndexRequestParameters, GetIndexResponse, IGetIndexResponse>(
				createIndexRequest,
				(p, d) => this.RawDispatch.IndicesGetDispatchAsync<GetIndexResponse>(
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