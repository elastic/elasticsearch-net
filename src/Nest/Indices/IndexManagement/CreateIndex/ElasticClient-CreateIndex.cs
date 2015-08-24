using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null) => 
			this.Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse>(
				createIndexSelector?.InvokeOrDefault(new CreateIndexDescriptor().Index(indexName)),
				this.LowLevelDispatch.IndicesCreateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(ICreateIndexRequest createIndexRequest) => 
			this.Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse>(
				createIndexRequest,
				this.LowLevelDispatch.IndicesCreateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null) => 
			this.Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				createIndexSelector?.InvokeOrDefault(new CreateIndexDescriptor().Index(indexName)),
				this.LowLevelDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest createIndexRequest) => 
			this.Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				createIndexRequest,
				this.LowLevelDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>
			);
	}
}