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
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the create index operation</param>
		IIndicesOperationResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse CreateIndex(ICreateIndexRequest request);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null) =>
			this.CreateIndex(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public IIndicesOperationResponse CreateIndex(ICreateIndexRequest request) => 
			this.Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse>(
				request,
				this.LowLevelDispatch.IndicesCreateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null) => 
			this.CreateIndexAsync(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest request) => 
			this.Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				request,
				this.LowLevelDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>
			);
	}
}