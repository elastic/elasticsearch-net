using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="createIndexSelector">A descriptor that describes the parameters for the create index operation</param>
		IIndicesOperationResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse CreateIndex(ICreateIndexRequest createIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest createIndexRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null) =>
			this.CreateIndex(createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(ICreateIndexRequest createIndexRequest) => 
			this.Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse>(
				createIndexRequest,
				this.LowLevelDispatch.IndicesCreateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null) => 
			this.CreateIndexAsync(createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest createIndexRequest) => 
			this.Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				createIndexRequest,
				this.LowLevelDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>
			);
	}
}